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
    public void MoveSpotlight()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movement = movement.normalized;
        _spotlightCircle.position += movement * _spotlightMoveSpeed * Time.deltaTime;

        _spotlightObject.rotation = Quaternion.LookRotation(_spotlightCircle.position);
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
