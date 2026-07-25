using Content.Shared.Inventory;

namespace Content.Goobstation.Shared.StrangeMood;

public sealed class StrangeMoodSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;

    private EntityQuery<WoundableComponent> _woundQuery;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StrangeMoodComponent, ComponentInit>(OnComponentInit);

        _woundQuery = GetEntityQuery<WoundableComponent>();
    }

    private void OnComponentInit(Entity<StrangeMoodComponent> ent, ComponentInit args)
    {
        if (ent.Comp.SpawnWith is not null)
            _inventory.SpawnItemOnEntity(ent, ent.Comp.SpawnWith);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

    }
}
