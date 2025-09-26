using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    private GameManager _manager;
    private Color _orgininalColor;


    private void Start()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("GameManager");
        _manager = Manager.GetComponent<GameManager>();
        _orgininalColor = this.GetComponent<Light>().color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dancer"))
        {
            _manager.IsOccupied = true;
        }

        if (other.CompareTag("Spotlight"))
        {
            this.GetComponent<Light>().color = Color.Lerp(_orgininalColor, other.GetComponent<Light>().color, .5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dancer"))
        {
            _manager.IsOccupied = false;
        }

        if (other.CompareTag("Spotlight"))
        {
            this.GetComponent<Light>().color = _orgininalColor;
        }
    }
}
