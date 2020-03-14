using System;
using Guns;
using UnityEngine;

namespace Guns {
  [Serializable]
  public class GunInHand {
    public GunConfig Config;
    public Vector3 Position;
    public Vector3 Rotation;
  }
}