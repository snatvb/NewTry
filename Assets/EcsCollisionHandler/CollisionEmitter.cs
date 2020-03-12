using System;
using Leopotam.Ecs;
using UnityEngine;

namespace EcsCollisionHandler {
  public class CollisionEmitter {
    private static CollisionEmitter instance;
    private readonly EcsWorld world;

    private CollisionEmitter(EcsWorld world) {
      this.world = world;
    }

    public static CollisionEmitter GetInstance() {
      if (instance?.world == null) {
        throw new Exception("CollisionEmitter not initialize.");
      }

      return instance;
    }

    internal static CollisionEmitter Initialize(EcsWorld world) {
      if (instance != null) { return instance; }

      instance = new CollisionEmitter(world);
      return instance;
    }

    public void Emit<T>(GameObject gameObject, Collider collider) where T : struct, ITriggerEvent {
      var entity = world.NewEntity();
      ref var component = ref entity.Set<T>();
      component.colliderOwner = gameObject;
      component.collisionWith = collider.gameObject;
      component.enabled = collider.enabled;
      component.attachedRigidbody = collider.attachedRigidbody;
      component.isTrigger = collider.isTrigger;
      component.bounds = collider.bounds;
    }

    public void Emit<T>(GameObject gameObject, Collision collision) where T : struct, ICollisionEvent {
      var entity = world.NewEntity();
      ref var component = ref entity.Set<T>();
      component.colliderOwner = gameObject;
      component.collisionWith = collision.gameObject;
      component.impulse = collision.impulse;
      component.rigidbody = collision.rigidbody;
      component.relativeVelocity = collision.relativeVelocity;
      component.contactCount = collision.contactCount;
    }
  }
}