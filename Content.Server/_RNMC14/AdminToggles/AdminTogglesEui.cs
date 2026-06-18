
using Content.Server.Administration.Logs;
using Content.Server.EUI;
using Content.Server.Chat.Managers;
using Content.Shared._RNMC14.AdminToggles;
using Content.Shared.Eui;
using Robust.Shared.Configuration;

namespace Content.Server._RNMC14.AdminToggles
{
    public sealed class AdminTogglesEui : BaseEui
    {
        [Dependency] private readonly IConfigurationManager _cfg = default!;
        [Dependency] private readonly IChatManager _chat = default!;

        public AdminTogglesEui()
        {
            IoCManager.InjectDependencies(this);
        }

        public override void Opened()
        {
            StateDirty();
        }

        public override EuiStateBase GetNewState()
        {
            return new AdminToggleEuiState();
        }

        public override void HandleMessage(EuiMessageBase msg)
        {
            base.HandleMessage(msg);

            switch (msg)
            {
                case AdminToggleEuiMsg.ToggleDefibs ToggleDefibs:
                    {
                        var cvar = _cfg.GetCVar<bool>("rnmc.defibs_enabled");

                        _cfg.SetCVar("rnmc.defibs_enabled", !cvar);
                        _chat.SendAdminAlert($"rnmc.defibs_enabled = {cvar}");
                            break;
                    }
                case AdminToggleEuiMsg.ToggleHardcore ToggleHardcore:
                    {
                        var cvar = _cfg.GetCVar<bool>("rnmc.defibs_hardcore");

                        _cfg.SetCVar("rnmc.defibs_hardcore", !cvar);
                        _chat.SendAdminAlert($"rnmc.defibs_hardcore = {cvar}");
                            break;
                    }
            }
        }
    }
}
