using Components;
using Enemy.Components;
using Guns.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy.Systems {
  public class EnemyShootSystem : IEcsRunSystem {
    private EcsWorld world;
#pragma warning disable 649
    private EcsFilter<TransformComponent, StatsComponent, PlayerComponent> playersFilter;
    private EcsFilter<TransformComponent, EnemyComponent> enemiesFilter;
#pragma warning restore 649

    public void Run() {
      foreach (var index in playersFilter) {
        ref var transformComponent = ref playersFilter.Get1(index);
        ref var statsComponent = ref playersFilter.Get2(index);
        RunEnemies(index, transformComponent.Value, ref statsComponent);
      }
    }

    private void RunEnemies(int playerIndex, Transform playerTransofrm, ref StatsComponent playerStats) {
      foreach (var index in enemiesFilter) {
        ref var enemyTransformComponent = ref enemiesFilter.Get1(index);
        var enemyTransform = enemyTransformComponent.Value;
        ref var enemyComponent = ref enemiesFilter.Get2(index);
        float distance = Vector3.Distance(playerTransofrm.position, enemyTransform.position);
        if (enemyComponent.RadiusDetection >= distance) {
          Attack(playerIndex, playerTransofrm, enemyTransform, ref enemyComponent);
        } else {
          enemyComponent.IsAiming = false;
        }
      }
    }

    private static void LookAt(Transform playerTransofrm, Transform enemyTransform) {
      var lookPosition = playerTransofrm.position - enemyTransform.position;
      lookPosition.y = 0;
      enemyTransform.rotation = Quaternion.LookRotation(lookPosition);
    }

    private void Attack(
      int playerIndex,
      Transform playerTransofrm,
      Transform enemyTransform,
      ref EnemyComponent enemyComponent
    ) {
      if (enemyComponent.Gun.Has<GunComponent>()) {
        ref var gunComponent = ref enemyComponent.Gun.Set<GunComponent>();
        Vector3 direction = playerTransofrm.position - enemyTransform.position;
        RaycastHit hit;
        if (Physics.Raycast(enemyTransform.position, direction, out hit)) {
          if (hit.transform != playerTransofrm) {
            enemyComponent.IsAiming = false;
            return;
          }
        }

        enemyComponent.IsAiming = true;
        LookAt(playerTransofrm, enemyTransform);
        if (gunComponent.CurrentChargingTime >= gunComponent.ChargingTime) {
          Shot(playerIndex, ref hit, ref gunComponent);
        } else {
          gunComponent.CurrentChargingTime += Time.deltaTime;
        }
      }
    }

    private void Shot(int playerIndex, ref RaycastHit hit, ref GunComponent gunComponent) {
      gunComponent.CurrentChargingTime = 0;
      var enitity = world.NewEntity();
      ref var plasmaShotComponent = ref enitity.Set<PlasmaShotComponent>();
      plasmaShotComponent.Damage = gunComponent.Damage;
      plasmaShotComponent.Victim = playersFilter.Entities[playerIndex];
      gunComponent.Visual.Shot(gunComponent.Projectile, hit.collider.bounds.center);
    }
  }
}