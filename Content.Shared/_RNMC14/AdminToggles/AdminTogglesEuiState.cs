using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Shared._RNMC14.AdminToggles;

[Serializable, NetSerializable]
public sealed class AdminToggleEuiState : EuiStateBase
{
}

public static class AdminToggleEuiMsg
{
    [Serializable, NetSerializable]
    public sealed class ToggleDefibs : EuiMessageBase
    {
    }

    [Serializable, NetSerializable]
    public sealed class ToggleHardcore : EuiMessageBase
    {
    }

    [Serializable, NetSerializable]
    public sealed class ToggleDropshipWeedkillers : EuiMessageBase
    {
    }
}
