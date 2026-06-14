using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Shared._RNMC14.AdminNarrate;

public enum AdminNarrateType
{
    Ghosts,
    All
}

[Serializable, NetSerializable]
public sealed class AdminNarrateEuiState : EuiStateBase
{
}

public static class AdminNarrateEuiMsg
{
    [Serializable, NetSerializable]
    public sealed class DoAnnounce : EuiMessageBase
    {
        public bool OOC;
        public string Announcement = default!;
        public AdminNarrateType AnnounceType;
    }
}
