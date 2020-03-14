using System;
using Animation;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Helpers {
  public static class ComponentAdder {
    public static void AddRigidbodyComponent(Transform transform, ref EcsEntity entity) {
      var rigidbody = transform.GetComponent<Rigidbody>();
      if (rigidbody == null) {
        throw new ArgumentNullException(nameof(rigidbody), "Entity prefab must require rigidbody component");
      }

      ref var component = ref entity.Set<RigidbodyComponent>();
      component.Value = rigidbody;
    }

    public static void AddColliderComponent(Transform transform, ref EcsEntity entity) {
      var collider = transform.GetComponent<CapsuleCollider>();
      if (collider == null) {
        throw new ArgumentNullException(nameof(collider), "Entity prefab must require collider component");
      }

      ref var component = ref entity.Set<ColliderComponent>();
      component.Value = collider;
    }

    public static void AddTransformComponent(Transform transform, ref EcsEntity entity) {
      ref var component = ref entity.Set<TransformComponent>();
      component.Value = transform;
    }

    public static void AddAnimationComponent(Transform transform, ref EcsEntity entity) {
      var animator = transform.GetComponent<Animator>();
      if (animator == null) {
        throw new ArgumentNullException(nameof(animator), "Player prefab must require animator component");
      }

      ref var component = ref entity.Set<AnimationComponent>();
      component.Animator = animator;
    }
  }
}