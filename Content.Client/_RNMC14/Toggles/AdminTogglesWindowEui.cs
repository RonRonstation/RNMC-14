using Content.Client.Eui;
using Content.Shared.Eui;
using Robust.Client.UserInterface.Controls;
using Content.Shared._RNMC14.AdminToggles;

namespace Content.Client._RNMC14.Toggles
{
    public sealed class AdminTogglesEui : BaseEui
    {
        private readonly AdminTogglesWindow _window;

        public AdminTogglesEui()
        {
            _window = new AdminTogglesWindow();
            _window.OnClose += () => SendMessage(new CloseEuiMessage());
            _window.ToggleDefibs.OnPressed += ToggleDefibsOnPressed;
            _window.Hardcore.OnPressed += ToggleHardcoreOnPressed;
            _window.DropshipWeedkillers.OnPressed += ToggleDropshipWeedkillersOnPressed;
        }

        private void ToggleDefibsOnPressed(BaseButton.ButtonEventArgs obj)
        {
            SendMessage(new AdminToggleEuiMsg.ToggleDefibs{});
        }

        private void ToggleHardcoreOnPressed(BaseButton.ButtonEventArgs obj)
        {
            SendMessage(new AdminToggleEuiMsg.ToggleHardcore{});
        }

        private void ToggleDropshipWeedkillersOnPressed(BaseButton.ButtonEventArgs obj)
        {
            SendMessage(new AdminToggleEuiMsg.ToggleDropshipWeedkillers {});
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
