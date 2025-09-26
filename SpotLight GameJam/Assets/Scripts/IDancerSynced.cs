using UnityEngine;

public interface IDancerSynced
{
    public void ValueChanged(float previousValue, float newValue, float animationProgress);
    public void AnimationEnded();
    public void Changee();
}
