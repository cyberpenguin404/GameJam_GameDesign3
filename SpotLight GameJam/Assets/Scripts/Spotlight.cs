using UnityEngine;

public class Spotlight : MonoBehaviour
{
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerScript>().SelectSpotlight(this);
    }
}
