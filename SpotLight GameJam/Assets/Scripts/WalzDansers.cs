using UnityEngine;

public class WalzDansers : Dancer, IDancerSynced
{
    public float AnimationValue { get; private set; }
    [SerializeField]
    private Transform _dancerObject;
    [SerializeField]
    private float _circleSpeed;
    [SerializeField]
    private float _dancerCircleSpeed;
    [SerializeField]
    private float _circleRadius;

    public override void AnimationEnded()
    {
    }

    public override void HandleDancing(float previousValue, float newValue, float animationProgress)
    {
        float circleX = Mathf.Sin(animationProgress * 2 * Mathf.PI) * _circleRadius;
        float circleY = Mathf.Sin(animationProgress * 2 * Mathf.PI * 2) * _circleRadius / 2f;

        _dancerObject.transform.position = new Vector3(circleX, 0, circleY) + transform.position;
        _dancerObject.rotation = Quaternion.Euler(0, newValue * _dancerCircleSpeed, 0);
    }
}
