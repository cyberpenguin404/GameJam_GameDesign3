using System;
using UnityEngine;

public abstract class Dancer : MonoBehaviour, IIluminatable, IDancerSynced
{
    public States CurrentState { get; internal set; } = States.MovingToStart;
    public float MoveToStartTimer { get; internal set; } = 0f;
    public float MoveToStartTime { get; internal set; } = 3f;
    public Vector3 StartPosition { get; set; }
    public abstract void AnimationEnded();

    public virtual void Changee()
    {
        if (CurrentState == States.WaitingToDance)
        {
            CurrentState = States.Dancing;
        }
    }

    public abstract void OnIlluminated();

    public virtual void ValueChanged(float previousValue, float newValue, float animationProgress)
    {
        if (StartPosition != Vector3.zero && CurrentState == States.MovingToStart)
        {
            MoveToStartTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, StartPosition, MoveToStartTimer / MoveToStartTime);
            if (Vector3.Distance(StartPosition, transform.position) < 0.1f)
            {
                CurrentState = States.WaitingToDance;
            }
        }
        if (CurrentState == States.Dancing)
        {
            HandleDancing(previousValue, newValue, animationProgress);
        }
    }

    public abstract void HandleDancing(float previousValue, float newValue, float animationProgress);
}

public enum States
{
    Dancing,
    MovingToStart,
    WaitingToDance,
    Idling
}
