using Content.Client.Eui;
using Content.Shared.Eui;
using Content.Shared._RNMC14.AdminNarrate;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client._RNMC14.Narrate
{
    public sealed class AdminNarrateEui : BaseEui
    {
        private readonly AdminNarrateWindow _window;

        public AdminNarrateEui()
        {
            _window = new AdminNarrateWindow();
            _window.OnClose += () => SendMessage(new CloseEuiMessage());
            _window.NarrateButton.OnPressed += AnnounceButtonOnOnPressed;
        }

        private void AnnounceButtonOnOnPressed(BaseButton.ButtonEventArgs obj)
        {
            SendMessage(new AdminNarrateEuiMsg.DoAnnounce
            {
                OOC = _window.OOC.Pressed,
                Announcement = Rope.Collapse(_window.Narrate.TextRope),
                AnnounceType = (AdminNarrateType)(_window.NarrateMethod.SelectedMetadata ?? AdminNarrateType.Ghosts),
            });

        }

        public override void Opened()
        {
            _window.OpenCentered();
        }

        public override void Closed()
        {
            _window.Close();
        }
    }
}
