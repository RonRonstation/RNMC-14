using Content.Server._RNMC14.AdminNarrate;
using Content.Server.EUI;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server.Administration.Commands
{
    [AdminCommand(AdminFlags.Admin)]
    public sealed class NarrateUiCommand : LocalizedEntityCommands
    {
        [Dependency] private readonly EuiManager _euiManager = default!;

        public override string Command => "narrateui";

        public override void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var player = shell.Player;
            if (player == null)
            {
                shell.WriteLine(Loc.GetString($"shell-cannot-run-command-from-server"));
                return;
            }

            var ui = new AdminNarrateEui();
            _euiManager.OpenEui(ui, player);
        }
    }
}
