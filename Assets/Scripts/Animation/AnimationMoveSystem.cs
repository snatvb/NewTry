using System;
using Leopotam.Ecs;
using Move;
using UnityEngine;

namespace Animation {
  public class AnimationMoveSystem : IEcsRunSystem {
    private static readonly int Moving = Animator.StringToHash("Moving");
    // private static readonly int IsJump = Animator.StringToHash("IsJump");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private EcsFilter<MoveComponent, AnimationComponent> filter;

    public void Run() {
      foreach (var index in filter) {
        ref var moveComponent = ref filter.Get1(index);
        ref var animationComponent = ref filter.Get2(index);

        animationComponent.Animator.SetFloat(Moving, Math.Abs(moveComponent.Horizontal));
        // animationComponent.Animator.SetBool(IsJump, moveComponent.IsJump);
        animationComponent.Animator.SetBool(IsFalling, !moveComponent.IsGround);
      }
    }
  }
}