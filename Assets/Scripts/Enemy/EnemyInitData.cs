using System;
using Components;
using Guns;
using UnityEngine;

namespace Enemy {
  [Serializable]
  public struct EnemyConfig {
    public Vector3 Position;
    public Quaternion Rotation;
    public float RadiusDetection;
    public float MaxSpeed;
    public float Acceleration;
    public StatsComponent Stats;
    public GunInHand Gun;
  }
  
  [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Create Data Script")]
  public class EnemyInitData : ScriptableObject {
    
    public Transform Prefab;
    public EnemyConfig[] EnemySpawns;
  }
}