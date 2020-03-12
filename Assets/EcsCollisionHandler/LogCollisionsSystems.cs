using Leopotam.Ecs;
using UnityEngine;

namespace EcsCollisionHandler {
  public class LogCollisionsSystems : IEcsRunSystem, IEcsInitSystem {
    private EcsFilter<CollisionEnterEvent> filterEnterCollision;
    private EcsFilter<CollisionStayEvent> filterStayCollision;
    private EcsFilter<CollisionExitEvent> filterExitCollision;
    private EcsFilter<TriggerEnterEvent> filterEnterTrigger;
    private EcsFilter<TriggerStayEvent> filterStayTrigger;
    private EcsFilter<TriggerExitEvent> filterExitTrigger;

    public void Init() {
      #if UNITY_EDITOR
      Debug.Log("Collision logger inited. Don't forget please remove this in build.");
      #endif
    }

    public void Run() {
      #if UNITY_EDITOR
      ForEachTrigger(filterEnterTrigger);
      ForEachTrigger(filterStayTrigger);
      ForEachTrigger(filterExitTrigger);
      ForEachCollision(filterEnterCollision);
      ForEachCollision(filterStayCollision);
      ForEachCollision(filterExitCollision);
      #endif
    }

    private void ForEachTrigger<T>(EcsFilter<T> filter) where T : struct, ITriggerEvent {
      foreach (var index in filter) {
        ref var hannpend = ref filter.Get1(index);
        Log($"Trigger event {typeof(T).Name}", hannpend.colliderOwner, hannpend.collisionWith);
      }
    }

    private void ForEachCollision<T>(EcsFilter<T> filter) where T : struct, ICollisionEvent {
      foreach (var index in filter) {
        ref var hannpend = ref filter.Get1(index);
        Log($"Collision event {typeof(T).Name}", hannpend.colliderOwner, hannpend.collisionWith);
      }
    }

    private void Log(string type, GameObject from, GameObject to) {
      Debug.Log($"[LogCollisionsSystems] {type} {from.name} with ${to.name}");
    }
  }
}