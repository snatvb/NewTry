using System;

namespace Components {
  [Serializable]
  public struct ShooterComponent {
    public float FireRate; // Per sec
    public float Damage;
  }
}