using UnityEngine;

public interface IDancerSynced
{
    public void ValueChanged(float previousValue, float newValue);
    public void AnimationEnded();
}
