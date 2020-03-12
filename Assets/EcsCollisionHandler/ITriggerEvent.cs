using UnityEngine;

namespace EcsCollisionHandler {
  public interface ITriggerEvent {
    GameObject colliderOwner { get; set; }
    GameObject collisionWith { get; set; }
    bool enabled { get; set; }
    Rigidbody attachedRigidbody { get; set; }
    bool isTrigger { get; set; }
    float contactOffset { get; set; }
    Bounds bounds { get; set; }
  }
}