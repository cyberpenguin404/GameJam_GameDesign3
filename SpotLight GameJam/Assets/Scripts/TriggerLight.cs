using UnityEngine;

public class TriggerLight : MonoBehaviour
{
    private GameManager _manager;
    public Color _Color;


    private void Start()
    {
        GameObject Manager = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Spotlight"))
        //{
        //    this.GetComponent<Light>().color = Color.Lerp(_orgininalColor, other.GetComponent<Light>().color, .5f);
        //}
        if (other.CompareTag("Dancer"))
        {
            other.GetComponent<Dancer>().OnStartIlluminated(_Color);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dancer"))
        {
            other.GetComponent<Dancer>().OnIlluminated(_Color);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Spotlight"))
        //{
        //    this.GetComponent<Light>().color = _orgininalColor;
        //}
        if (other.CompareTag("Dancer"))
        {
            other.GetComponent<Dancer>().OnEndIlluminated(_Color);
        }
    }
}
