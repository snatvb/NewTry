using Leopotam.Ecs;

namespace EcsCollisionHandler {
  public static class EcsRegisterExtention {
    public static EcsSystems RegisterCollisionEmitter(this EcsSystems ecsSystems) {
      CollisionEmitter.Initialize(ecsSystems.World);
      InjectOneFrameComponents(ecsSystems);
      return ecsSystems;
    }

    private static void InjectOneFrameComponents(EcsSystems ecsSystems) {
      ecsSystems.OneFrame<CollisionEnterEvent>();
      ecsSystems.OneFrame<CollisionStayEvent>();
      ecsSystems.OneFrame<CollisionExitEvent>();
      ecsSystems.OneFrame<TriggerEnterEvent>();
      ecsSystems.OneFrame<TriggerStayEvent>();
      ecsSystems.OneFrame<TriggerExitEvent>();
    }
  }
}