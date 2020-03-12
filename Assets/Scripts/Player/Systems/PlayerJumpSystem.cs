using Components;
using Leopotam.Ecs;
using Move;
using UnityEngine;

namespace Player.Systems {
  public class PlayerJumpSystem : IEcsRunSystem {
    private EcsFilter<MoveComponent, RigidbodyComponent> filter;

    public void Run() {
      foreach (var index in filter) {
        ref MoveComponent moveComponent = ref filter.Get1(index);
        ref RigidbodyComponent rigidbodyComponent = ref filter.Get2(index);
        if (moveComponent.IsJump && moveComponent.IsGround) {
          rigidbodyComponent.Value.AddForce(moveComponent.JumpForce * Vector3.up, ForceMode.Impulse);
        }
      }
    }
  }
}