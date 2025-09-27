using System;
using UnityEngine;

public abstract class Dancer : MonoBehaviour, IIluminatable, IDancerSynced
{
    public float AssignedStartTime {  get; set; }
    public float AssignedEndTime { get; set; }
    private float DanceDuration { get {  return AssignedEndTime - AssignedStartTime; } }
    public float AnimationValue { get; private set; }
    private int _currentCycle;
    private int Cycles { get { return Mathf.FloorToInt((AnimationValue - AssignedStartTime) / DanceDuration); } }
    private float LocalProgress { get { return ((AnimationValue - AssignedStartTime) % DanceDuration) / DanceDuration; } }
    public States CurrentState { get; internal set; } = States.MovingToStart;

    private float _moveTimer;
    public float MoveTime = 3f;

    public Vector3 StartPosition { get; set; }
    public Vector3 ExitPosition { get; set; }

    public float IlluminationValue { get; private set; }
    public float IlluminationHP;
    private void Awake()
    {
        _currentCycle = Cycles;
    }
    public abstract void AnimationEnded();

    public virtual void Changee()
    {
        if (CurrentState == States.WaitingToDance)
        {
            CurrentState = States.Dancing;
        }
    }

    public virtual void OnIlluminated()
    {
        IlluminationValue += Time.deltaTime;
        Debug.Log($"{this.gameObject.name} is being Illuminated");
        if (IlluminationValue > IlluminationHP)
        {
            CurrentState = States.MoveToExit;
            _moveTimer = 0;
        }
    }

    public virtual void ValueChanged(float previousValue, float newValue)
    {
        AnimationValue = newValue;
        if (_currentCycle != Cycles)
        {
            Changee();
            _currentCycle = Cycles;
        }
        if (CurrentState == States.MoveToExit)
        {
            _moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, ExitPosition, _moveTimer / MoveTime);
            if (Vector3.Distance(ExitPosition, transform.position) < 0.1f)
            {
                RemoveDancer();
            }
        }
        if (StartPosition != Vector3.zero && CurrentState == States.MovingToStart)
        {
            _moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, StartPosition, _moveTimer / MoveTime);
            if (Vector3.Distance(StartPosition, transform.position) < 0.1f)
            {
                CurrentState = States.WaitingToDance;
            }
        }
        if (CurrentState == States.Dancing)
        {
            HandleDancing(previousValue, newValue, LocalProgress);
        }
    }

    private void RemoveDancer()
    {
        DancerManager dancerManager = GameObject.FindGameObjectWithTag("DancerManager").GetComponent<DancerManager>();
        dancerManager.RemoveDancer(this);
        Destroy(gameObject);
    }

    public abstract void HandleDancing(float previousValue, float newValue, float animationProgress);
}

public enum States
{
    Dancing,
    MovingToStart,
    WaitingToDance,
    MoveToExit,
    Idling
}
