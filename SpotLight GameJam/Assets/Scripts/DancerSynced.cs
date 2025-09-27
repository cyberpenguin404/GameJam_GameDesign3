using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DancerSynced
{
    public float AnimationValue { get; private set; }
    public float AnimationDuration { get; private set; }
    public float AnimationProgress { get { return AnimationValue /  AnimationDuration; } }
    public List<IDancerSynced> Observers { get; private set; } = new List<IDancerSynced>();
    public void UpdateAnimation(float frameDuration)
    {
        float previousValue = AnimationValue;
        AnimationValue += frameDuration;
        foreach (IDancerSynced observer in Observers)
        {
            observer.ValueChanged(previousValue, AnimationValue);
        }
    }
    public DancerSynced()
    {
    }

    public void ResetAnimation()
    {
        AnimationValue = 0;
    }
    public void AddObserver(IDancerSynced observer)
    {
        Observers.Add(observer);
    }
    public void RemoveObserver(IDancerSynced observer)
    {
        if (Observers.Contains(observer))
            Observers.Remove(observer);
    }
}
