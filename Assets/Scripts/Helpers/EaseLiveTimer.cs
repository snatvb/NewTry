using System;
using UnityEngine;

public class EaseLiveTimer {
  public enum State {
    Grow,
    Lives,
    Dying,
    Died,
  }

  private readonly float inTime;
  private readonly float outTime;
  private readonly float liveTime;
  private float age;
  private float livesFactor;
  private State state;

  public event Action<State> OnChange;

  public EaseLiveTimer(float inOutTime, float liveTime) : this(inOutTime, inOutTime, liveTime) { }

  public EaseLiveTimer(float inTime, float outTime, float liveTime) {
    this.inTime = inTime;
    this.outTime = outTime;
    this.liveTime = liveTime;

    age = liveTime;
  }

  public float GetLiveTime() {
    return liveTime;
  }

  public float GetLivesFactor() {
    return livesFactor;
  }

  public float GetAge() {
    return age;
  }
  
  public State GetState() {
    return state;
  }

  public void Update() {
    switch (state) {
      case State.Grow:
        HandleGrow();
        break;
      case State.Dying:
        HandleDying();
        break;
      case State.Lives:
        HandleLives();
        break;
      case State.Died:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void HandleLives() {
    age -= Time.deltaTime;

    if (age <= 0) {
      SetState(State.Dying);
    }
  }

  private void HandleGrow() {
    livesFactor = Mathf.Clamp01(livesFactor + Time.deltaTime / inTime);

    if (livesFactor == 1) {
      SetState(State.Lives);
    }
  }

  private void HandleDying() {
    livesFactor = Mathf.Clamp01(livesFactor - Time.deltaTime / outTime);

    if (livesFactor == 0) {
      SetState(State.Died);
    }
  }

  private void SetState(State state) {
    if (state != this.state) {
      this.state = state;
      OnChange?.Invoke(state);
    }
  }
}