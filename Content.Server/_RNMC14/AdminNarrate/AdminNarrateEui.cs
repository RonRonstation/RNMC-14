using Content.Server.Administration.Managers;
using Content.Server.Chat.Managers;
using Content.Server.EUI;
using Content.Shared._RNMC14.AdminNarrate;
using Content.Shared.Administration;
using Content.Shared.Chat;
using Content.Shared.Eui;
using Content.Shared.Ghost;
using Robust.Server.Player;
using Robust.Shared.GameObjects;
using Robust.Shared.Player;
using Robust.Shared.Utility;

namespace Content.Server._RNMC14.AdminNarrate
{
    public sealed class AdminNarrateEui : BaseEui
    {
        [Dependency] private readonly IAdminManager _adminManager = default!;
        [Dependency] private readonly IChatManager _chatManager = default!;
        [Dependency] private readonly IEntityManager _entity = default!;
        private EntityQueryEnumerator<GhostComponent> _ghostQuery;

        public AdminNarrateEui()
        {
            IoCManager.InjectDependencies(this);
        }

        public override void Opened()
        {
            StateDirty();
        }

        public override EuiStateBase GetNewState()
        {
            return new AdminNarrateEuiState();
        }

        public override void HandleMessage(EuiMessageBase msg)
        {
            base.HandleMessage(msg);

            Filter filter = Filter.Empty();
            filter.AddWhereAttachedEntity(_entity.HasComponent<GhostComponent>);

            switch (msg)
            {
                case AdminNarrateEuiMsg.DoAnnounce doAnnounce:
                    if (!_adminManager.HasAdminFlag(Player, AdminFlags.Admin))
                    {
                        Close();
                        break;
                    }
                    string message;
                    string oocAnnounce = $"OOC Announcement:{Environment.NewLine}{doAnnounce.Announcement}";
                    if (doAnnounce.OOC)
                        message = oocAnnounce;
                    else
                        message = doAnnounce.Announcement;

                        string wrappedMessage = Loc.GetString("rnmc-chat-manager-server-wrap-message-header", ("message", FormattedMessage.EscapeText(message)));


                    switch (doAnnounce.AnnounceType)
                    {
                        case AdminNarrateType.All:
                            _chatManager.ChatMessageToAll(ChatChannel.Server, message, wrappedMessage, EntityUid.Invalid, hideChat: false, recordReplay: true, Color.FromHex("#5959e1"));
                            break;
                        case AdminNarrateType.Ghosts:
                            _chatManager.ChatMessageToManyFiltered(filter, Shared.Chat.ChatChannel.Server, message, wrappedMessage, new EntityUid(), false, true, Color.FromHex("#5959e1"));
                            break;
                    }

                    StateDirty();
                    break;
            }
        }
    }
}
