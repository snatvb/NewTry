using System.Runtime.CompilerServices;
using Components;
using Leopotam.Ecs;
using Move;
using UnityEngine;

namespace Player.Systems {
  public class PlayerRunSystem : IEcsRunSystem {
    private EcsFilter<MoveComponent, RigidbodyComponent, TransformComponent, PlayerComponent> filter;

    public void Run() {
      foreach (var index in filter) {
        ref MoveComponent moveComponent = ref filter.Get1(index);
        if (!moveComponent.IsGround || moveComponent.IsJump) {
          return;
        }

        ref RigidbodyComponent rigidbodyComponent = ref filter.Get2(index);
        ref TransformComponent transform = ref filter.Get3(index);
        if (rigidbodyComponent.Value.velocity.x < moveComponent.MaxSpeed) {
          Vector3 newVeslocity = rigidbodyComponent.Value.velocity;
          newVeslocity.x = Mathf.Clamp(
            newVeslocity.x + moveComponent.Horizontal * moveComponent.Acceleration * Time.deltaTime,
            -moveComponent.MaxSpeed,
            moveComponent.MaxSpeed
          );
          rigidbodyComponent.Value.velocity = newVeslocity;
        }

        int horizontalNormalized = Normalize(moveComponent.Horizontal);
        if (horizontalNormalized != 0) {
          transform.Value.rotation = new Quaternion(0, 90 * horizontalNormalized, 0, 90);
        }
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Normalize(float value) {
      if (value == 0) {
        return 0;
      }

      return value > 0 ? 1 : -1;
    }
  }
}