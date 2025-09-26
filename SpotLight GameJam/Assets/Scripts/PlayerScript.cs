using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private Spotlight _currentlySelectedSpotlight;
    [SerializeField]
    private bool _currentlyControllingSpotlight;
    void Update()
    {
        if (_currentlyControllingSpotlight)
        { 
            HandleSpotlight();
        }
        else
        { 
            HandleMovement();
        }

    }

    private void HandleSpotlight()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentlyControllingSpotlight = false;
        }
        _currentlySelectedSpotlight.MoveSpotlight();
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentlyControllingSpotlight = true;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        _transform.position += movement * _speed * Time.deltaTime;
    }

    public void SelectSpotlight(Spotlight spotlight)
    {
        _currentlySelectedSpotlight = spotlight;
    }
}
