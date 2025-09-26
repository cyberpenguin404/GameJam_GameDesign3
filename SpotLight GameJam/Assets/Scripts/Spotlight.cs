using UnityEngine;
using UnityEngine.UIElements;

public class Spotlight : MonoBehaviour
{
    [SerializeField]
    private float _spotlightMoveSpeed;
    [SerializeField]
    private Transform _spotlightObject;
    [SerializeField]
    private Transform _spotlightLight;
    [SerializeField]
    private Transform _spotlightCircle;
    [SerializeField]
    private Vector2 _spotlightRange;

    public void MoveSpotlight()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized;
        _spotlightCircle.position += movement * _spotlightMoveSpeed * Time.deltaTime;

        if(_spotlightCircle.position.x > _spotlightRange.x )
        {
            _spotlightCircle.position = new Vector3(_spotlightRange.x, _spotlightCircle.position.y, _spotlightCircle.position.z);
        }
        if (_spotlightCircle.position.x < -_spotlightRange.x)
        {
            _spotlightCircle.position = new Vector3(-_spotlightRange.x, _spotlightCircle.position.y, _spotlightCircle.position.z);
        }
        if (_spotlightCircle.position.z > _spotlightRange.y)
        {
            _spotlightCircle.position = new Vector3(_spotlightCircle.position.x, _spotlightCircle.position.y, _spotlightRange.y);
        }
        if (_spotlightCircle.position.z < -_spotlightRange.y)
        {
            _spotlightCircle.position = new Vector3(_spotlightCircle.position.x, _spotlightCircle.position.y, -_spotlightRange.y);
        }

        _spotlightObject.rotation = Quaternion.LookRotation(transform.position - _spotlightCircle.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerScript>(out PlayerScript player))
            player.SelectSpotlight(this);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerScript>(out PlayerScript player))
        player.SelectSpotlight(null);
    }
}
