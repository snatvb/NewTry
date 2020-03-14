using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlasmaLineVisual : MonoBehaviour {
    
    private LineRenderer lineRenderer;
    private float liveTime = 2;
    private float opacityChangeDuration = .2f;
    private static readonly int Alpha = Shader.PropertyToID("Alpha");
    private EaseLiveTimer timer;
    private bool initialized;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.SetFloat(Alpha, 0);
    }

    private void HandleChageTimerState(EaseLiveTimer.State state) {
        if (state == EaseLiveTimer.State.Died) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (!initialized) {
            return;
        }
        
        timer.Update();
        Animate();
    }

    private void Animate() {
        var state = timer.GetState();
        if (state == EaseLiveTimer.State.Grow || state == EaseLiveTimer.State.Dying) {
            lineRenderer.material.SetFloat(Alpha, timer.GetLivesFactor());
        }
    }

    public void SetLiveTime(float time) {
        liveTime = time;
    }
    
    public void SetOpacityChangeDuration(float time) {
        opacityChangeDuration = time;
    }
    
    public void SetPoistions(Vector3 from, Vector3 to) {
        lineRenderer.SetPositions(new []{from, to});
    }

    public void Initialize() {
        timer = new EaseLiveTimer(opacityChangeDuration, liveTime);
        timer.OnChange += HandleChageTimerState;
        initialized = true;
    }
}
