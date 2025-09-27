using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dancer : MonoBehaviour, IIluminatable, IDancerSynced
{
    #region Appearance
    [SerializeField] private Material _ClothesMaterial;
    [SerializeField] private List<Renderer> _dancerRenderers = new List<Renderer>();
    internal List<Material> _instanceMaterials = new List<Material>();
    private float _maxEmission = 10;
    public ParticleSystem ShiningParticles;
    #endregion
    #region AnimationsAndDancing
    private const float BPM = 60f;
    public readonly float SecondsPerBeat = 60f / BPM;
    public float AssignedStartBeat { get; set; }
    public float AssignedEndBeat { get; set; }
    private float DanceDuration { get { return AssignedEndBeat - AssignedStartBeat; } }
    public float AnimationValue { get; private set; }
    public float AnimationValueBeats { get { return AnimationValue / SecondsPerBeat; } }
    private int _currentCycle;
    private int Cycles { get { return Mathf.FloorToInt((AnimationValueBeats - AssignedStartBeat) / DanceDuration); } }
    private float LocalProgress { get { return ((AnimationValueBeats - AssignedStartBeat) % DanceDuration) / DanceDuration; } }
    #endregion
    public Color DanceColor;
    public States CurrentState { get; internal set; } = States.MovingToStart;

    private float _moveTimer;
    public float MoveTime = 3f;

    public Vector3 StartPosition { get; set; }
    public Vector3 ExitPosition { get; set; }

    public int _illuminationModifier = 0;
    public float IlluminationValue { get; private set; }
    public float IlluminationHP;
    public void Initiate()
    {
        _currentCycle = Cycles;
        foreach (Renderer renderer in _dancerRenderers)
        {
            _instanceMaterials.Add(renderer.material);
            renderer.material.SetFloat("_EmissiveIntensity", 0);
            renderer.material.color = DanceColor;
        }

        var emission = ShiningParticles.emission;
        emission.enabled = false;
    }
    public abstract void AnimationEnded();

    public virtual void Changee()
    {
        Debug.Log("Changee called");
        if (CurrentState == States.WaitingToDance)
        {
            CurrentState = States.Dancing;
        }
    }

    public virtual void OnIlluminated(Color color)
    {
        if (DanceColor == Color.white)
        {
            ApplyLight();
            return;
        }
        if (color == DanceColor)
        {
            ApplyLight();
        }
    }

    private void ApplyLight()
    {
        IlluminationValue += Time.deltaTime;
        Debug.Log($"{this.gameObject.name} is being Illuminated with {_illuminationModifier} modifier");
        if (_illuminationModifier >= 2 && (DanceColor == Color.white || DanceColor == Color.magenta))
        {
            foreach (Material material in _instanceMaterials)
            {
                material.SetColor("_EmissiveColor", Color.magenta * _maxEmission);
            }
        }
        if (IlluminationValue > IlluminationHP)
        {
            CurrentState = States.MoveToExit;
            _moveTimer = 0;
        }
    }

    public void OnStartIlluminated(Color color)
    {
        _illuminationModifier += 1;
        if (DanceColor == Color.white)
        {
            StartIlluminating();
            return;
        }
        if (color == DanceColor)
        {
            StartIlluminating();
        }
    }

    private void StartIlluminating()
    {
        var emission = ShiningParticles.emission;
        emission.enabled = true;
    }

    public void OnEndIlluminated(Color color)
    {
        _illuminationModifier -= 1;
        foreach (Material material in _instanceMaterials)
        {
            material.SetColor("_EmissiveColor", Color.black);
        }

        if (_illuminationModifier == 0)
        {
            var emission = ShiningParticles.emission;
            if (ShiningParticles != null)
                emission.enabled = false;
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
