using System;
using Components;
using Leopotam.Ecs;
using Move;
using UnityEngine;

namespace Systems {
  public class GroundCheckSystem : IEcsRunSystem {
    private EcsFilter<ColliderComponent, MoveComponent> filter;
    private readonly LayerMask layerMask;

    public GroundCheckSystem(LayerMask mask) {
      layerMask = mask;
    }

    public void Run() {
      foreach (var index in filter) {
        ref ColliderComponent collider = ref filter.Get1(index);
        ref MoveComponent moveComponent = ref filter.Get2(index);

        float radius = GetRadius(collider.Value);
        float distance = collider.Value.bounds.extents.y + radius;
        moveComponent.IsGround = Physics.SphereCast(
          collider.Value.bounds.center,
          radius,
          Vector3.down,
          out _,
          distance,
          layerMask
        );
      }
    }

    private static float GetRadius(Collider collider) {
      switch (collider) {
        case CapsuleCollider capsuleCollider:
          return capsuleCollider.radius;
        case SphereCollider sphereCollider:
          return sphereCollider.radius;
        default:
          float x = Math.Abs(collider.bounds.center.x - collider.bounds.extents.x) * 2;
          float y = Math.Abs(collider.bounds.center.y - collider.bounds.extents.y) * 2;
          float z = Math.Abs(collider.bounds.center.z - collider.bounds.extents.z) * 2;
          return (x + y + z) / 3;
      }
    }

    public void OnDrawGizmozSelected() {
      Debug.Log("Called");
    }
  }
}