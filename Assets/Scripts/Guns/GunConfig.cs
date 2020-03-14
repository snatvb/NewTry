using System;
using UnityEngine;

namespace Guns {
  [CreateAssetMenu(fileName = "New Gun", menuName = "Guns/Create New Gun")]
  public class GunConfig : ScriptableObject {
    public Transform Prefab;
    public Transform Projectile;
    public Transform Flash;
    public float Damage;
    public float FireRate;
    public float ChargingTime;
  }
}