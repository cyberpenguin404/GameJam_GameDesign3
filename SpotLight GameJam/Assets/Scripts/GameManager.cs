using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _score;
    public bool IsOccupied = false;

    // Update is called once per frame
    void Update()
    {
        if (IsOccupied)
        {
            _score++;
        }
    }
}
