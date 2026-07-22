using Content.Server._RMC14.Marines;
using Content.Server.AlertLevel;
using Content.Shared._RMC14.AlertLevel;
using Content.Shared._RMC14.Dropship.Utility.Systems;
using Content.Shared.Administration;
using Content.Shared.Body.Components;
using Content.Shared.Chemistry;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Components.SolutionManager;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Medical.Cryogenics;
using Robust.Shared.Audio;
using Robust.Shared.Console;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Timing;
using System.Globalization;
using System.Linq;

namespace Content.Server.Administration.Commands
{
    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresHijackLineCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_hijack";
        public override string Description => Loc.GetString("cmd-areshijackline-desc");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RMC14/Announcements/ARES/hijack.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rmc-announcement-dropship-hijack"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresDistressBeaconCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_distressbeacon";
        public override string Description => Loc.GetString("rnmc-announcement-distress-beacon");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RNMC14/Announcements/ARES/distressbeacon.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-distress-beacon"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresEncryptedSignalCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_encryptedsignal";
        public override string Description => Loc.GetString("rnmc-announcement-distress-received");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RNMC14/Announcements/ARES/distressreceived.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-distress-received"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresEvacuateCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_evacuation";
        public override string Description => Loc.GetString("rnmc-announcement-evacuate");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RNMC14/Announcements/ARES/evacuate.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-evacuate"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresEvacuateCancelCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_evac_cancelled";
        public override string Description => Loc.GetString("rnmc-announcement-evacuate-cancelled");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RMC14/Announcements/ARES/evacuate_cancelled.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-evacuate-cancelled"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresEvacuateCompleteCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_evac_complete";
        public override string Description => Loc.GetString("rnmc-announcement-evacuate-complete");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RMC14/Announcements/ARES/evacuation_complete.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-evacuate-complete"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresEvacuateConfirmedCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_line_evac_confirmed";
        public override string Description => Loc.GetString("rnmc-announcement-evacuate-confirmed");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RMC14/Announcements/ARES/evacuation_confirmed.ogg");

            _announce.AnnounceARES(null, Loc.GetString("rnmc-announcement-evacuate-confirmed"), sound);
        }
    }

    [AdminCommand(AdminFlags.Admin)]
    public sealed class AresGQFullCallCommand : LocalizedEntityCommands
    {
        public override string Command => "ares_GQ_full_call";
        public override string Description => Loc.GetString("rmc-announcement-general-quarters");

        [Dependency] private readonly MarineAnnounceSystem _announce = default!;

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var sound = new SoundPathSpecifier("/Audio/_RMC14/Announcements/ARES/GQfullcall.ogg");
            _announce.AnnounceARES(null, Loc.GetString("rmc-announcement-general-quarters"), sound);
        }
    }
}
