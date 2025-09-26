using Unity.VisualScripting;
using UnityEngine;

public class BalletDancer : Dancer
{
    [SerializeField]
    private float _poseChangeTime;
    [SerializeField]
    private float _poseChangeSpeed;
    [SerializeField]
    private float _poseChangeRange;
    [SerializeField]
    private float _rotationSpeed;
    private float _timeSinceLastPoseChange;
    private float _lerpTimer;
    private Vector3 _targetPosition = Vector3.zero;
    private Vector2 _arenaLimits = new Vector2(13, 9);
    public override void AnimationEnded()
    {
    }

    public override void OnIlluminated()
    {
    }

    public override void ValueChanged(float previousValue, float newValue)
    {
        _timeSinceLastPoseChange += newValue - previousValue;

        transform.rotation *= Quaternion.Euler(0, (newValue - previousValue) * _rotationSpeed, 0);

        if (_timeSinceLastPoseChange > _poseChangeTime)
        {
            ChangePose();
        }
    }

    private void ChangePose()
    {
        _lerpTimer += Time.deltaTime * _poseChangeSpeed;
        if (_targetPosition == Vector3.zero)
        {
            Vector3 newTargetPosition;
            do
            {
                Vector2 randomTarget = Random.insideUnitCircle * _poseChangeRange;

                newTargetPosition = new Vector3(transform.position.x + randomTarget.x, transform.position.y, transform.position.z + randomTarget.y);
            }
            while (!IsInsideArena(newTargetPosition));
            _targetPosition = newTargetPosition;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpTimer);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            _timeSinceLastPoseChange = 0;
            _lerpTimer = 0;
            _targetPosition = Vector3.zero;
        }
    }

    private bool IsInsideArena(Vector3 targetPosition)
    {
        bool xInsideArena = targetPosition.x < _arenaLimits.x && targetPosition.x > -_arenaLimits.x;
        bool yInsideArena = targetPosition.y < _arenaLimits.y && targetPosition.y > -_arenaLimits.y;
        return xInsideArena && yInsideArena;
    }
}
