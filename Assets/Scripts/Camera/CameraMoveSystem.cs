using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Camera {
  public class CameraMoveSystem : IEcsInitSystem, IEcsRunSystem {
    private readonly EcsWorld world = null;
    private readonly EcsFilter<CameraComponent, TransformComponent> filter = null;

    public void Init() {
      var entity = world.NewEntity();
      entity.Set<CameraComponent>();
      ref var transformComponent = ref entity.Set<TransformComponent>();
      GameObject camera = GetCamera();
      if (camera == null) {
        Debug.LogWarning("Can't get camera");
        return;
      }

      transformComponent.Value = camera.transform;
    }

    public void Run() {
      foreach (var index in filter) {
        ref var cameraComponent = ref filter.Get1(index);
        if (!cameraComponent.isMoving) {
          return;
        }
        ref var transform = ref filter.Get2(index);

        transform.Value.position = Vector3.Lerp(
          transform.Value.position,
          cameraComponent.position + cameraComponent.offset,
          cameraComponent.smoothSpeed
        );
      }
    }

    private static GameObject GetCamera() {
      return UnityEngine.Camera.main.gameObject;
    }

  }
}