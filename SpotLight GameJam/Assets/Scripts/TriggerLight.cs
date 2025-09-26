using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    private GameManager _manager;

    private void Start()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("GameManager");
        _manager = Manager.GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dancer"))
        {
            _manager.IsOccupied = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dancer"))
        {
            _manager.IsOccupied = false;
        }
    }
}
