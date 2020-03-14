using Components;
using Enemy.Components;
using Guns;
using Helpers;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy {
  public class EnemySpawnSystem : IEcsInitSystem {
    private EnemyInitData enemyInitData;
    private readonly EcsWorld world;
    
    public EnemySpawnSystem(EnemyInitData enemyInitData) {
      this.enemyInitData = enemyInitData;
    }

    public void Init() {
      foreach (EnemyConfig enemyConfig in enemyInitData.EnemySpawns) {
        Transform transform = Object.Instantiate(enemyInitData.Prefab, enemyConfig.Position, enemyConfig.Rotation);
        var entity = world.NewEntity();
        var config = enemyConfig;
        ref EnemyComponent component = ref entity.Set<EnemyComponent>();
        component.RadiusDetection = enemyConfig.RadiusDetection;

        if (config.Gun != null) {
          AddGun(transform, ref component, ref config);
        }
        
        AddOtherComponents(transform, ref entity, ref config);
        
        enemyInitData = null;
      }
    }

    private void AddGun(Transform transform, ref EnemyComponent enemyComponent, ref EnemyConfig config) {
      var gunEntity = GunInitializer.Create(world, config.Gun);
      Transform rigthHandTransform = TransformRefs.GetRightHand(transform);
      ref var transformComponent = ref gunEntity.Set<TransformComponent>();
      transformComponent.Value.SetParent(rigthHandTransform);
      enemyComponent.Gun = gunEntity;
    }

    private static void AddOtherComponents(Transform transform, ref EcsEntity entity, ref EnemyConfig enemyConfig) {
      AddStatsComponent(ref entity, ref enemyConfig);
      
      ComponentAdder.AddTransformComponent(transform, ref entity);
      ComponentAdder.AddAnimationComponent(transform, ref entity);
      ComponentAdder.AddRigidbodyComponent(transform, ref entity);
      ComponentAdder.AddColliderComponent(transform, ref entity);
    }

    private static void AddStatsComponent(ref EcsEntity entity, ref EnemyConfig enemyConfig) {
      ref StatsComponent component = ref entity.Set<StatsComponent>();
      component.Health = enemyConfig.Stats.Health;
      component.MaxHealth = enemyConfig.Stats.MaxHealth;
    }
  }
}