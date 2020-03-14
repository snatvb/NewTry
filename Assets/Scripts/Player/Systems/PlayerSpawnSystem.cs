using Camera;
using Components;
using Helpers;
using Leopotam.Ecs;
using Move;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.Systems {
  public class PlayerSpawnSystem : IEcsInitSystem {
    private PlayerInitData playerInitData;
    private readonly EcsWorld world;

    public PlayerSpawnSystem(PlayerInitData playerInitData) {
      this.playerInitData = playerInitData;
    }

    public void Init() {
      Transform transform = Spawn();
      var entity = world.NewEntity();
      // Add components
      AddMoveComponent(ref entity);
      AddStatsComponent(ref entity);
      AddPlayerComponent(transform, ref entity);
      AddTargetCameraFollowComponent(transform, ref entity);
      ComponentAdder.AddTransformComponent(transform, ref entity);
      ComponentAdder.AddAnimationComponent(transform, ref entity);
      ComponentAdder.AddRigidbodyComponent(transform, ref entity);
      ComponentAdder.AddColliderComponent(transform, ref entity);
      playerInitData = null;
    }

    private void AddStatsComponent(ref EcsEntity entity) {
      ref StatsComponent component = ref entity.Set<StatsComponent>();
      component.Health = playerInitData.Stats.Health;
      component.MaxHealth = playerInitData.Stats.MaxHealth;
    }

    private Transform Spawn() {
      return Object.Instantiate(playerInitData.prefab, playerInitData.spawnPosition, playerInitData.spawnRotation);
    }

    private void AddPlayerComponent(Transform transform, ref EcsEntity entity) {
      entity.Set<PlayerComponent>();
    }
    
    private void AddTargetCameraFollowComponent(Transform transform, ref EcsEntity entity) {
      ref var component = ref entity.Set<CameraTargetFollowComponent>();
      component.smooth = playerInitData.cameraSmooth;
      component.transform = transform;
      component.offset = playerInitData.cameraOffset;
    }

    private void AddMoveComponent(ref EcsEntity entity) {
      ref var component = ref entity.Set<MoveComponent>();
      component.MaxSpeed = playerInitData.MaxSpeed;
      component.Acceleration = playerInitData.Acceleration;
      component.JumpForce = playerInitData.JumpForce;
      component.IsFixXPosition = true;
    }
  }
}