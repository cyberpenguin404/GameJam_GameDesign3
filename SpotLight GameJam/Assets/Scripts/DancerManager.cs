using System.Collections.Generic;
using UnityEngine;

public class DancerManager : MonoBehaviour
{
    private int _currentWaveIndex;
    public List<Wave> Waves = new List<Wave>();
    private List<Dancer> _dancerList = new List<Dancer>();

    public List<GameObject> DancerTypes = new List<GameObject>();
    public List<Transform> StartPositions = new List<Transform>();
    private int _startPositionIndex = 0;
    [SerializeField] 
    private Transform _entrance;
    [SerializeField]
    private Transform _exit;
    public DancerSynced _dancerSynced = new DancerSynced();
    public void RemoveDancer(Dancer dancer)
    {
        if (_dancerList.Contains(dancer))
        {
            _dancerList.Remove(dancer);

            _dancerSynced.RemoveObserver(dancer);
        }
    }
    private void Awake()
    {
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        if (_currentWaveIndex > _dancerList.Count)
        {
            Debug.Log("Finished all waves");
            return;
        }
        foreach (Guest guest in Waves[_currentWaveIndex].Guests)
        {
            SpawnDancer(guest.Dancer, guest.StartPosition, guest.IlluminationHP, guest.DanceTimings.x, guest.DanceTimings.y);
            _currentWaveIndex++;
        }
    }

    private void Update()
    {
        _dancerSynced.UpdateAnimation(Time.deltaTime);
        if (_dancerList.Count == 0)
        {
            SpawnNextWave();
        }
    }
    
    private void SpawnDancer(GameObject dancer, Vector3 startPosition, float illumantionHP, float startTime, float endTime)
    {
        GameObject newDancerObject = Instantiate(dancer, _entrance.position, Quaternion.identity);
        Dancer newDancer = newDancerObject.GetComponent<Dancer>();

        newDancer.StartPosition = startPosition;
        newDancer.ExitPosition = _exit.position;
        newDancer.AssignedStartTime = startTime;
        newDancer.AssignedEndTime = endTime;
        newDancer.IlluminationHP = illumantionHP;

        _dancerSynced.AddObserver(newDancer);
        _dancerList.Add(newDancer);
    }
}
[System.Serializable]
public class Guest
{
    public GameObject Dancer;
    public Vector2 DanceTimings;
    public Vector3 StartPosition;
    public int IlluminationHP;
}
[System.Serializable]
public class Wave
{
    public List<Guest> Guests;
}
