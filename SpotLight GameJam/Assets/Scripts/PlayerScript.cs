using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private Transform _transform;

    private Spotlight _currentlySelectedSpotlight;
    private bool _currentlyControllingCamera;
    void Update()
    {

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        movement = movement.normalized;

        _transform.position += movement * _speed * Time.deltaTime;

    }

    public void SelectSpotlight(Spotlight spotlight)
    {
        _currentlySelectedSpotlight = spotlight;
    }
}
