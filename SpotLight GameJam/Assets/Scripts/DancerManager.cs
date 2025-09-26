using System.Collections.Generic;
using UnityEngine;

public class DancerManager : MonoBehaviour
{
    [SerializeField]
    public List<Dancer> _dancerList = new List<Dancer>();
    public DancerSynced _dancerSynced;
    private void Awake()
    {
        _dancerSynced = new DancerSynced();
        foreach (IDancerSynced observer in _dancerList)
        {
            _dancerSynced.AddObserver(observer);
        }
    }
    private void Update()
    {
        _dancerSynced.UpdateAnimation(Time.deltaTime);
    }
}
