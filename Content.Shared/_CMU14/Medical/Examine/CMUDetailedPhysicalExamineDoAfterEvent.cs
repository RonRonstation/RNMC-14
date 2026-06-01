using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared._CMU14.Medical.Examine;

[Serializable, NetSerializable]
public sealed partial class CMUDetailedPhysicalExamineDoAfterEvent : SimpleDoAfterEvent;
