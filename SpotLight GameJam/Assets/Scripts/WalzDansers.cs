using UnityEngine;

public class WalzDansers : Dancer
{
    public float IlluminationValue {  get; private set; }
    public float AnimationValue { get; private set; }
    [SerializeField]
    private Transform _dancerObject;
    [SerializeField]
    private float _circleSpeed;
    [SerializeField]
    private float _dancerCircleSpeed;
    [SerializeField]
    private float _circleRadius;
    public override void OnIlluminated()
    {
        IlluminationValue += Time.deltaTime;
    }

    public override void AnimationEnded()
    {
    }

    public override void ValueChanged(float previousValue, float newValue)
    {
        float circleX = Mathf.Sin(newValue * _circleSpeed) * _circleRadius;
        float circleY = Mathf.Sin(newValue * _circleSpeed * 2) * _circleRadius / 2f;

        _dancerObject.transform.position = new Vector3(circleX, 1, circleY) + transform.position;
        _dancerObject.rotation = Quaternion.Euler(0, newValue * _dancerCircleSpeed, 0);
    }
}
