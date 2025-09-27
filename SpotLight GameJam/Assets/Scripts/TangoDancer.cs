using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TangoDancer : Dancer
{
    public List<Vector3> DancePositions = new List<Vector3>();
    private int _dancePositionIndex;
    private Vector3 _targetPosition;
    private Color PrimaryColor;
    public Color SecondaryColor;
    [SerializeField]
    private float _rotationSpeed;

    public override void Initiate()
    {
        base.Initiate();
        PrimaryColor = DanceColor;
    }
    public override void AnimationEnded()
    {
    }

    public override void Changee()
    {
        base.Changee();
        if (CurrentState == States.Dancing) {
            _targetPosition = DancePositions[_dancePositionIndex];
            _dancePositionIndex += 1;
            if (_dancePositionIndex >= DancePositions.Count)
            {
                _dancePositionIndex = 0;
            }

            DanceColor = DanceColor == SecondaryColor ? PrimaryColor : SecondaryColor;
            foreach (Material material in _instanceMaterials)
            {
                material.color = DanceColor;
                material.SetColor("_EmissiveColor", DanceColor * 0.5f);
            }
        }
    }
    public override void HandleDancing(float previousValue, float newValue, float animationProgress)
    {
        transform.rotation *= Quaternion.Euler(0, (newValue - previousValue) * _rotationSpeed, 0);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, animationProgress);
    }
}
