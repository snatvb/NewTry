using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GunVisual : MonoBehaviour {
    private Animator animator;
    [SerializeField] private Transform shotAnchor;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Shot(Transform projectailPrefab, Vector3 position) {
        var projectail = Instantiate(projectailPrefab, Vector3.zero, Quaternion.identity);
        PlasmaLineVisual lineVisual = projectail.GetComponent<PlasmaLineVisual>();
        lineVisual.SetPoistions(shotAnchor.position, position);
        lineVisual.SetLiveTime(3);
        lineVisual.Initialize();
    }
}
