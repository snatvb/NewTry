using Leopotam.Ecs;

namespace Enemy.Components {
  public struct EnemyComponent {
    public float RadiusDetection;
    public EcsEntity Gun;
    public bool IsAiming;
  }
}