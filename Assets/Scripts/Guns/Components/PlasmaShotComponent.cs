using Leopotam.Ecs;
using UnityEngine;

namespace Guns.Components {
  public struct PlasmaShotComponent {
    public EcsEntity Victim;
    public float Damage;
  }
}