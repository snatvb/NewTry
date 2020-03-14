using Camera;
using Helpers;
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
      AddMoveComponent(entity);
      AddPlayerComponent(transform, entity);
      AddTargetCameraFollowComponent(transform, entity);
      ComponentAdder.AddTransformComponent(transform, entity);
      ComponentAdder.AddAnimationComponent(transform, entity);
      ComponentAdder.AddRigidbodyComponent(transform, entity);
      ComponentAdder.AddColliderComponent(transform, entity);
    }

    private Transform Spawn() {
      return Object.Instantiate(playerInitData.prefab, playerInitData.spawnPosition, playerInitData.spawnRotation);
    }

    private void AddPlayerComponent(Transform transform, EcsEntity entity) {
      entity.Set<PlayerComponent>();
    }
    
    private void AddTargetCameraFollowComponent(Transform transform, EcsEntity entity) {
      ref var component = ref entity.Set<CameraTargetFollowComponent>();
      component.smooth = playerInitData.cameraSmooth;
      component.transform = transform;
      component.offset = playerInitData.cameraOffset;
    }

    private void AddMoveComponent(EcsEntity entity) {
      ref var component = ref entity.Set<MoveComponent>();
      component.MaxSpeed = playerInitData.MaxSpeed;
      component.Acceleration = playerInitData.Acceleration;
      component.JumpForce = playerInitData.JumpForce;
      component.IsFixXPosition = true;
    }
  }
}