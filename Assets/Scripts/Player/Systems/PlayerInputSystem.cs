using Leopotam.Ecs;
using Move;
using UnityEngine;

namespace Player.Systems {
  public class PlayerInputSystem : IEcsRunSystem {
    private EcsFilter<MoveComponent> moveFilter;

    public void Run() {
      foreach (var index in moveFilter) {
        ref MoveComponent moveComponent = ref moveFilter.Get1(index);
        HorizontalInputHandle(ref moveComponent);
        JumpInputHandle(ref moveComponent);
      }
    }

    private static void HorizontalInputHandle(ref MoveComponent moveComponent) {
      moveComponent.Horizontal = Input.GetAxis("Horizontal");
    }
    
    private static void JumpInputHandle(ref MoveComponent moveComponent) {
      moveComponent.IsJump = Input.GetKeyDown(KeyCode.Space);
    }
  }
}