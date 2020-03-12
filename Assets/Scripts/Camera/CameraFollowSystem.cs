using Leopotam.Ecs;
using UnityEngine;

namespace Camera {
  public class CameraFollowSystem : IEcsRunSystem {
    private readonly EcsFilter<CameraTargetFollowComponent> filter = null;
    private readonly EcsFilter<CameraComponent> filterCamera = null;

    public void Run() {
      if (filter.GetEntitiesCount() == 0) {
        return;
      }

      var targetFollow = filter.Get1(0);
      RunCamera(ref targetFollow);
    }

    private void RunCamera(ref CameraTargetFollowComponent targetFollow) {
      foreach (int index in filterCamera) {
        ref var cameraComponent = ref filterCamera.Get1(index);
        cameraComponent.smoothSpeed = targetFollow.smooth;
        cameraComponent.position = targetFollow.transform.position;
        cameraComponent.offset = targetFollow.offset;
        cameraComponent.isMoving = true;
      }
    }
  }
}