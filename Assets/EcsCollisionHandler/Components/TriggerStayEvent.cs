using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcsCollisionHandler {
  public struct TriggerStayEvent : ITriggerEvent {
    public GameObject colliderOwner { get; set; }
    public GameObject collisionWith { get; set; }
    public bool enabled { get; set; }
    public Rigidbody attachedRigidbody { get; set; }
    public bool isTrigger { get; set; }
    public float contactOffset { get; set; }
    public Bounds bounds { get; set; }
  }
}