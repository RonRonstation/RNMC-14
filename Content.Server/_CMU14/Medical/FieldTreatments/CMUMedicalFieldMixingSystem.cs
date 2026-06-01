using Content.Shared._CMU14.Medical.FieldTreatments;
using Content.Shared._RMC14.Marines.Skills;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.Stacks;

namespace Content.Server._CMU14.Medical.FieldTreatments;

public sealed partial class CMUMedicalFieldMixingSystem : EntitySystem
{
    [Dependency] private SharedPopupSystem _popup = default!;
    [Dependency] private SkillsSystem _skills = default!;
    [Dependency] private SharedStackSystem _stacks = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CMUMedicalIngredientComponent, AfterInteractEvent>(OnIngredientAfterInteract);
    }

    public int ResolveIngredientUnitCost(int medicalSkill)
    {
        return medicalSkill switch
        {
            <= 1 => 5,
            2 => 2,
            _ => 1,
        };
    }

    public bool TryMixTreatment(
        EntityUid user,
        EntityUid ingredientUid,
        EntityUid baseUid,
        out EntityUid? product)
    {
        product = null;

        if (!TryComp<CMUMedicalIngredientComponent>(ingredientUid, out var ingredient) ||
            !TryComp<CMUMedicalMixingBaseComponent>(baseUid, out var mixingBase) ||
            !TryComp<StackComponent>(ingredientUid, out var ingredientStack) ||
            !TryComp<StackComponent>(baseUid, out var baseStack))
        {
            return false;
        }

        var cost = ResolveIngredientUnitCost(_skills.GetSkill(user, ingredient.Skill));
        if (ingredientStack.Count < cost || baseStack.Count < 1)
            return false;

        var productId = mixingBase.Kind switch
        {
            CMUFieldTreatmentBaseKind.Gauze => ingredient.GauzeProduct,
            CMUFieldTreatmentBaseKind.TraumaDressing => ingredient.TraumaProduct,
            _ => default,
        };

        if (productId == default)
            return false;

        if (!_stacks.Use(ingredientUid, cost, ingredientStack))
            return false;

        if (!_stacks.Use(baseUid, 1, baseStack))
            return false;

        var spawned = Spawn(productId, Transform(user).Coordinates);
        product = spawned;
        _stacks.TryMergeToHands(spawned, user);
        return true;
    }

    private void OnIngredientAfterInteract(Entity<CMUMedicalIngredientComponent> ent, ref AfterInteractEvent args)
    {
        if (args.Handled || !args.CanReach || args.Target is not { } target)
            return;

        if (!TryMixTreatment(args.User, ent.Owner, target, out _))
            return;

        _popup.PopupEntity(Loc.GetString("cmu-field-treatment-mixed"), args.User, args.User);
        args.Handled = true;
    }
}
