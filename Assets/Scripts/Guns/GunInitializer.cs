using Guns.Components;
using Helpers;
using Leopotam.Ecs;
using UnityEngine;

namespace Guns {
  public static class GunInitializer {
    public static EcsEntity Create(EcsWorld world, GunInHand gunInHand) {
      Transform transform = Object.Instantiate(
        gunInHand.Config.Prefab,
        gunInHand.Position,
        Quaternion.Euler(gunInHand.Rotation)
      );
      return CreateEntity(world, gunInHand.Config, transform);
    }

    public static EcsEntity Create(EcsWorld world, GunConfig config) {
      Transform transform = Object.Instantiate(config.Prefab, Vector3.zero, Quaternion.identity);
      return CreateEntity(world, config, transform);
    }

    private static EcsEntity CreateEntity(EcsWorld world, GunConfig config, Transform transform) {
      var entity = world.NewEntity();
      ComponentAdder.AddTransformComponent(transform, ref entity);
      ComponentAdder.AddAnimationComponent(transform, ref entity);
      AddGunComponent(transform, config, entity);
      return entity;
    }

    private static void AddGunComponent(Transform transform, GunConfig config, EcsEntity entity) {
      ref GunComponent component = ref entity.Set<GunComponent>();
      component.Visual = transform.GetComponent<GunVisual>();
      component.Damage = config.Damage;
      component.FireRate = config.FireRate;
      component.Projectile = config.Projectile;
      component.ChargingTime = config.ChargingTime;
    }
  }
}