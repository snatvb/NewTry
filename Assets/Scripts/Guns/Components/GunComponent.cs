using UnityEngine;

namespace Guns.Components {
  public enum GunType {
    Plasma
  }

  public struct GunComponent {
    public GunType type;
    public GunVisual Visual;
    public Transform Projectile;
    public float Damage;
    public float FireRate;
    public float ChargingTime;
    public float CurrentChargingTime;
  }
}