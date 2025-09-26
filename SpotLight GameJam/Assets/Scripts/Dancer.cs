using UnityEngine;

public abstract class Dancer : MonoBehaviour, IIluminatable, IDancerSynced
{
    public abstract void AnimationEnded();

    public abstract void OnIlluminated();

    public abstract void ValueChanged(float previousValue, float newValue);
}
