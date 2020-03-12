using Leopotam.Ecs;
using UnityEngine;

namespace Camera {
  [EcsAutoResetCheck]
  public struct CameraTargetFollowComponent {
    public Transform transform;
    public float smooth;
    public Vector3 offset;
    
    public static void CustomReset (ref CameraTargetFollowComponent c) {
      c.transform = null;
    }
  }
}
