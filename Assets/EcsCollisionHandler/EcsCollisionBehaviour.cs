using System;
using UnityEngine;

namespace EcsCollisionHandler {
  public class EcsCollisionBehaviour : MonoBehaviour {
    [SerializeField] private bool OnCollisionEnterEnabled;
    [SerializeField] private bool OnCollisionStayEnabled;
    [SerializeField] private bool OnCollisionExitEnabled;
    [SerializeField] private bool OnTriggerEnterEnabled;
    [SerializeField] private bool OnTriggerStayEnabled;
    [SerializeField] private bool OnTriggerExitEnabled;

    private void OnCollisionEnter(Collision other) {
      if (OnCollisionEnterEnabled) {
        Emit<CollisionEnterEvent>(other);
      }
    }

    private void OnCollisionStay(Collision other) {
      if (OnCollisionStayEnabled) {
        Emit<CollisionStayEvent>(other);
      }
    }

    private void OnCollisionExit(Collision other) {
      if (OnCollisionExitEnabled) {
        Emit<CollisionExitEvent>(other);
      }
    }

    private void OnTriggerEnter(Collider other) {
      if (OnTriggerEnterEnabled) {
        Emit<TriggerEnterEvent>(other);
      }
    }

    private void OnTriggerStay(Collider other) {
      if (OnTriggerStayEnabled) {
        Emit<TriggerStayEvent>(other);
      }
    }

    private void OnTriggerExit(Collider other) {
      if (OnTriggerExitEnabled) {
        Emit<TriggerExitEvent>(other);
      }
    }

    private void Emit<T>(Collision other) where T : struct, ICollisionEvent {
      CollisionEmitter.GetInstance()
       .Emit<T>(gameObject, other);
    }

    private void Emit<T>(Collider other) where T : struct, ITriggerEvent {
      CollisionEmitter.GetInstance()
       .Emit<T>(gameObject, other);
    }
  }
}