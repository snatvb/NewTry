using UnityEngine;

namespace EcsCollisionHandler {
  public interface ICollisionEvent {
    GameObject colliderOwner { get; set; }
    GameObject collisionWith { get; set; }
    Vector3 impulse {get; set;}
    Rigidbody rigidbody {get; set;}
    int contactCount { get; set; }
    Vector3 relativeVelocity { get; set; }
  }
}