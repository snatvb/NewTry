using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player/Create Data Script")]
public class PlayerInitData : ScriptableObject {
  public Vector3 spawnPosition;
  public Quaternion spawnRotation;
  public Transform prefab;
  public float MaxSpeed;
  public float Acceleration;
  public float JumpForce;
  public Vector3 cameraOffset;
  public float cameraSmooth;
}