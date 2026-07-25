using Content.Shared.Construction.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Dictionary;

namespace Content.Shared.Random;

/// <summary>
/// Linter-friendly version of weightedRandom for Construction prototypes.
/// </summary>
[Prototype]
public sealed partial class WeightedRandomConstructionPrototype : IWeightedRandomPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField("weights", customTypeSerializer: typeof(PrototypeIdDictionarySerializer<float, ConstructionPrototype>))]
    public Dictionary<string, float> Weights { get; private set; } = new();
}
