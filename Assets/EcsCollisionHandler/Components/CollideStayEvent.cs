using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcsCollisionHandler {
  public struct CollisionStayEvent : ICollisionEvent {
    public GameObject colliderOwner { get; set; }
    public GameObject collisionWith { get; set; }
    public Vector3 impulse { get; set; }
    public Rigidbody rigidbody { get; set; }
    public int contactCount { get; set; }
    public Vector3 relativeVelocity { get; set; }
  }
}