using Components;
using Guns.Components;
using Leopotam.Ecs;

namespace Guns.Systems {
  public class PlasmaShotSystem : IEcsRunSystem {
    private EcsWorld world;
    private EcsFilter<PlasmaShotComponent> filter;

    public void Run() {
      foreach (var index in filter) {
        ref var plasmaShot = ref filter.Get1(index);
        if (plasmaShot.Victim.Has<TransformComponent>() && plasmaShot.Victim.Has<StatsComponent>()) {
          ref var transformComponent = ref plasmaShot.Victim.Set<TransformComponent>();
          ref var statsComponent = ref plasmaShot.Victim.Set<StatsComponent>();

          statsComponent.Health -= plasmaShot.Damage;
        }
      }
    }
  }
}