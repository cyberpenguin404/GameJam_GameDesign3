using System.Collections.Generic;
using UnityEngine;

public class DancerManager : MonoBehaviour
{
    public List<GameObject> DancerTypes = new List<GameObject>();
    private List<Dancer> _dancerList = new List<Dancer>();
    public List<Transform> StartPositions = new List<Transform>();
    private int _startPositionIndex = 0;
    [SerializeField] 
    private Transform _entrance;
    [SerializeField]
    private Transform _exit;
    public DancerSynced _dancerSynced = new DancerSynced();
    private void Awake()
    {
        //_dancerSynced = new DancerSynced();
        //foreach (IDancerSynced observer in _dancerList)
        //{
        //    _dancerSynced.AddObserver(observer);
        //}
        SpawnDancer();
        SpawnDancer();
    }
    private void Update()
    {
        _dancerSynced.UpdateAnimation(Time.deltaTime);
    }
    private void SpawnDancer()
    {
        GameObject newDancerObject = Instantiate(DancerTypes[Random.Range(0, DancerTypes.Count)], _entrance.position, Quaternion.identity);
        Dancer newDancer = newDancerObject.GetComponent<Dancer>();
        newDancer.StartPosition = StartPositions[_startPositionIndex].position;
        _dancerSynced.AddObserver(newDancer);
        _startPositionIndex++;
        if (_startPositionIndex >= StartPositions.Count)
        {
            _startPositionIndex = 0;
        }
    }
}
