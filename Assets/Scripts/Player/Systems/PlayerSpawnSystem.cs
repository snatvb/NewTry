using System;
using Animation;
using Camera;
using Components;
using Leopotam.Ecs;
using Move;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.Systems {
  public class PlayerSpawnSystem : IEcsInitSystem {
    private readonly PlayerInitData playerInitData;
    private readonly EcsWorld world;

    public PlayerSpawnSystem(PlayerInitData playerInitData) {
      this.playerInitData = playerInitData;
    }

    public void Init() {
      Transform transform = Spawn();
      var entity = world.NewEntity();
      // Add components
      AddMoveComponent(transform, entity);
      AddPlayerComponent(transform, entity);
      AddTransformComponent(transform, entity);
      AddAnimationComponent(transform, entity);
      AddRigidbodyComponent(transform, entity);
      AddColliderComponent(transform, entity);
      AddTargetCameraFollowComponent(entity);
    }

    private Transform Spawn() {
      return Object.Instantiate(playerInitData.prefab, playerInitData.spawnPosition, playerInitData.spawnRotation);
    }

    private void AddAnimationComponent(Transform transform, EcsEntity entity) {
      var animator = transform.GetComponent<Animator>();
      if (animator == null) {
        throw new ArgumentNullException(nameof(animator), "Player prefab must require animator component");
      }

      ref var component = ref entity.Set<AnimationComponent>();
      component.Animator = animator;
    }

    private void AddPlayerComponent(Transform transform, EcsEntity entity) {
      entity.Set<PlayerComponent>();
    }
    
    private void AddRigidbodyComponent(Transform transform, EcsEntity entity) {
      var rigidbody = transform.GetComponent<Rigidbody>();
      if (rigidbody == null) {
        throw new ArgumentNullException(nameof(rigidbody), "Player prefab must require rigidbody component");
      }

      ref var component = ref entity.Set<RigidbodyComponent>();
      component.Value = rigidbody;
    }
    
    private void AddColliderComponent(Transform transform, EcsEntity entity) {
      var collider = transform.GetComponent<CapsuleCollider>();
      if (collider == null) {
        throw new ArgumentNullException(nameof(collider), "Player prefab must require collider component");
      }

      ref var component = ref entity.Set<ColliderComponent>();
      component.Value = collider;
    }

    private void AddTargetCameraFollowComponent(EcsEntity entity) {
      ref var component = ref entity.Set<MoveComponent>();
      component.MaxSpeed = playerInitData.MaxSpeed;
      component.Acceleration = playerInitData.Acceleration;
      component.JumpForce = playerInitData.JumpForce;
    }

    private void AddMoveComponent(Transform transform, EcsEntity entity) {
      ref var component = ref entity.Set<CameraTargetFollowComponent>();
      component.smooth = playerInitData.cameraSmooth;
      component.transform = transform;
      component.offset = playerInitData.cameraOffset;
    }
    
    private void AddTransformComponent(Transform transform, EcsEntity entity) {
      ref var component = ref entity.Set<TransformComponent>();
      component.Value = transform;
    }
  }
}