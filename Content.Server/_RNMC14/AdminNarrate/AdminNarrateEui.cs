using Content.Server.Administration.Commands;
using Content.Server.Administration.Managers;
using Content.Server.Chat;
using Content.Server.Chat.Managers;
using Content.Server.Chat.Systems;
using Content.Server.EUI;
using Content.Server.Power.Components;
using Content.Shared._RNMC14.AdminNarrate;
using Content.Shared.Administration;
using Content.Shared.Eui;
using Content.Shared.Ghost;
using Robust.Server.Player;
using Robust.Shared.Player;
using Robust.Shared.Utility;

namespace Content.Server._RNMC14.AdminNarrate
{
    public sealed class AdminNarrateEui : BaseEui
    {
        [Dependency] private readonly IAdminManager _adminManager = default!;
        [Dependency] private readonly IChatManager _chatManager = default!;
        [Dependency] private readonly IPlayerManager _playerManager = default!;
        private readonly ChatSystem _chatSystem;
        private EntityQueryEnumerator<GhostComponent> _ghostQuery;

        public AdminNarrateEui()
        {
            IoCManager.InjectDependencies(this);
            _chatSystem = IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<ChatSystem>();
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
            Filter filter = Filter.Empty();

            while (_ghostQuery.MoveNext(out var uid, out var _))
            {
                _playerManager.TryGetSessionByEntity(uid, out var session);
                if (session != null)
                    filter.AddPlayer(session);
            }

            base.HandleMessage(msg);

            switch (msg)
            {
                case AdminNarrateEuiMsg.DoAnnounce doAnnounce:
                    if (!_adminManager.HasAdminFlag(Player, AdminFlags.Admin))
                    {
                        Close();
                        break;
                    }
                    string oocAnnounce = $"OOC Announcement:{Environment.NewLine}{doAnnounce.Announcement}";
                    string message = doAnnounce.OOC ? oocAnnounce : doAnnounce.Announcement;
                    string wrappedMessage = Loc.GetString("chat-manager-server-wrap-message", ("message", FormattedMessage.EscapeText(message)));


                    switch (doAnnounce.AnnounceType)
                    {
                        case AdminNarrateType.All:
                            _chatManager.DispatchServerAnnouncement(doAnnounce.Announcement, Color.FromHex("5959e1"));
                            break;
                        case AdminNarrateType.Ghosts:
                            _chatManager.ChatMessageToManyFiltered(filter, Shared.Chat.ChatChannel.Server, message, wrappedMessage, new EntityUid(), false, true, Color.FromHex("5959e1"));
                            break;
                    }

                    StateDirty();
                    break;
            }
        }
    }
}
