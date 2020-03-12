using Animation;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems {
  public class FixRampPositionSystem : IEcsRunSystem {
    private EcsFilter<TransformComponent, AnimationComponent> filter;

    public void Run() {
      foreach (var index in filter) {
        ref var transformComponent = ref filter.Get1(index);

        var x = transformComponent.Value.position.x;
        var y = transformComponent.Value.position.y;
        transformComponent.Value.position = new Vector3(x, y, 0f);
      }
    }
  }
}