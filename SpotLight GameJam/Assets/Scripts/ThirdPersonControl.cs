using UnityEngine;

public class ThirdPersonControl : MonoBehaviour
{
    [SerializeField] private Vector2 _limitY;
    [SerializeField] private float _speedRotate;
    [SerializeField] private Transform CameraY;

    private float _horizontalRotation;
    private float _verticalRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        _horizontalRotation += Input.GetAxis("Mouse X") * _speedRotate * Time.deltaTime;
        _verticalRotation -= Input.GetAxis("Mouse Y") * _speedRotate * Time.deltaTime;
        _verticalRotation = Mathf.Clamp(_verticalRotation, _limitY.x, _limitY.y);

        transform.rotation = Quaternion.Euler(0, _horizontalRotation, 0);
        CameraY.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}