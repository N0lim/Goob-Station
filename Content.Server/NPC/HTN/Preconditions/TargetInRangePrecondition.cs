// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 metalgearsloth <metalgearsloth@gmail.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Plykiya <58439124+Plykiya@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2024 plykiya <plykiya@protonmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Interaction; // goob edit start
using Content.Server.Stealth; // goob edit end
using Content.Shared.Stealth.Components;
using Robust.Shared.Map;

namespace Content.Server.NPC.HTN.Preconditions;

/// <summary>
/// Is the specified key within the specified range of us.
/// </summary>
public sealed partial class TargetInRangePrecondition : HTNPrecondition
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    private SharedTransformSystem _transform = default!;
    private StealthSystem _stealth = default!; // goob edit start
    private SharedInteractionSystem _interaction = default!; // goob edit end

    [DataField("targetKey", required: true)]
    public string TargetKey = default!;

    [DataField("rangeKey", required: true)]
    public string RangeKey = default!;

    [DataField("isUnobstructed")]
    public bool IsUnobstructed = false; // goob edit

    public override void Initialize(IEntitySystemManager sysManager)
    {
        base.Initialize(sysManager);
        _transform = sysManager.GetEntitySystem<SharedTransformSystem>();
        _stealth = sysManager.GetEntitySystem<StealthSystem>(); // goob edit start
        _interaction = sysManager.GetEntitySystem<SharedInteractionSystem>(); // goob edit end
    }

    public override bool IsMet(NPCBlackboard blackboard)
    {
        if (!blackboard.TryGetValue<EntityCoordinates>(NPCBlackboard.OwnerCoordinates, out var ownerCoordinates, _entManager)
        || !blackboard.TryGetValue<EntityUid>(TargetKey, out var target, _entManager)
        || !_entManager.TryGetComponent<TransformComponent>(target, out var targetXform)
        // goob edit - stealthed entities can't be seen by npcs
        || _entManager.TryGetComponent<StealthComponent>(target, out var stealth) && _stealth.GetVisibility(target, stealth) <= stealth.ExamineThreshold)
            return false;

        var range = blackboard.GetValueOrDefault<float>(RangeKey, _entManager);

        if (IsUnobstructed)
        {
            return _interaction.InRangeUnobstructed(_transform.ToMapCoordinates(ownerCoordinates), _transform.GetMapCoordinates(targetXform), range); // goob edit
        }
        else
        {
            return _transform.InRange(ownerCoordinates, targetXform.Coordinates, range);
        }
    }
}
