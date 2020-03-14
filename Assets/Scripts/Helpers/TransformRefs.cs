using System;
using UnityEngine;

namespace Helpers {
  public static class TransformRefs {
    public static Transform GetRightHand(Transform transform) {
      RightHandRef rightHandRef = transform.GetComponent<RightHandRef>();
      if (rightHandRef == null) {
        throw new Exception($"Object {transform} don't have {typeof(RightHandRef)} component");
      }

      return rightHandRef.RightHand;
    }
  }
}