using Content.Shared.ActionBlocker;
using Content.Shared.Popups;

namespace Content.Server.NPC.HTN.Preconditions;

/// <summary>
/// Checks if owner can interact with target
/// </summary>
public sealed partial class CanInteractWithTargetPrecondition : HTNPrecondition
{
    [Dependency] private readonly IEntityManager _entManager = default!;

    private ActionBlockerSystem _actionBlocker = default!;
    private SharedPopupSystem _popup = default!;

    [DataField("targetKey", required: true)] public string TargetKey = default!;

    public override void Initialize(IEntitySystemManager sysManager)
    {
        base.Initialize(sysManager);
        _actionBlocker = sysManager.GetEntitySystem<ActionBlockerSystem>();
        _popup = sysManager.GetEntitySystem<SharedPopupSystem>();
    }

    public override bool IsMet(NPCBlackboard blackboard)
    {
        if (!blackboard.TryGetValue<EntityUid>(NPCBlackboard.Owner, out var owner, _entManager))
            return false;

        if (!blackboard.TryGetValue<EntityUid>(TargetKey, out var target, _entManager))
            return false;

        return _actionBlocker.CanInteract(owner, target);
    }
}
