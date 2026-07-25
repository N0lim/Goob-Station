using Robust.Shared.Prototypes;
using Content.Shared.Construction.Prototypes;
using Content.Shared.Random;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Dictionary;

namespace Content.Goobstation.Shared.StrangeMood;

[RegisterComponent/*, NetworkedComponent*/]
public sealed partial class StrangeMoodComponent : Component
{
    /// <summary>
    /// Is strange mood active now
    /// </summary>
    [DataField]
    public bool IsActive = false;

    /// <summary>
    /// Probability to start strange mood every update
    /// </summary>
    [DataField]
    public float Probability = 0.0005555555f; // 1/1800, average in 30 minutes

    /// <summary>
    /// hallucination * multiplier + probability = actual probability for update
    /// </summary>
    [DataField]
    public float HallucinationMultiplier = 0;

    /// <summary>
    /// What prototype we consider as hallucinations
    /// </summary>
    public ProtoID<EntityPrototype> HallucinationID = "StatusEffectSeeingRainbow";

    /// <summary>
    /// How often we want to update
    /// </summary>
    [DataField]
    public TimeSpan UpdateRate = TimeSpan.FromSeconds(1);

    /// <summary>
    /// When we want to update next time
    /// </summary>
    [DataField]
    public TimeSpan NextUpdate = 0;

    /// <summary>
    /// What we want to spawn when component added or when entity spawned with it, usually it would be some drug to stop strange mood
    /// </summary>
    [DataField]
    public ProtoID<EntityPrototype>? SpawnWith = null;

    /// <summary>
    /// List of possible goals to make items when strange mood started
    /// </summary>
    [DataField(required: true)]
    public ProtoId<WeightedRandomConstructionPrototype> ToMake;
}
