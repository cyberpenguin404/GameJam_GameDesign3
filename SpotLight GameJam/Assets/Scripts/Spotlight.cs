using UnityEngine;
using UnityEngine.UIElements;

public class Spotlight : MonoBehaviour
{
    [SerializeField]
    private int _player;
    [SerializeField]
    private float _spotlightMoveSpeed;
    [SerializeField]
    private Transform _spotlightObject;
    [SerializeField]
    private Transform _spotlightCircle;
    [SerializeField]
    private Vector2 _spotlightRange;

    public void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(_player == 1 ? KeyCode.A : KeyCode.LeftArrow)) movement.x -= 1;
        if (Input.GetKey(_player == 1 ? KeyCode.D : KeyCode.RightArrow)) movement.x += 1;
        if (Input.GetKey(_player == 1 ? KeyCode.W : KeyCode.UpArrow)) movement.z += 1;
        if (Input.GetKey(_player == 1 ? KeyCode.S : KeyCode.DownArrow)) movement.z -= 1;


        movement = movement.normalized;
        _spotlightCircle.position += movement * _spotlightMoveSpeed * Time.deltaTime;

        Vector3 newPosition = _spotlightCircle.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -_spotlightRange.x, _spotlightRange.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -_spotlightRange.y, _spotlightRange.y);
        _spotlightCircle.position = newPosition;

        _spotlightObject.rotation = Quaternion.LookRotation(transform.position - _spotlightCircle.position);
    }
}
