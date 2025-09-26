using UnityEngine;

public class DancerManager : MonoBehaviour
{
    private DancerSynced _dancerSynced = new DancerSynced();
    private void Awake()
    {
        foreach (IDancerSynced observer in transform.GetComponentsInChildren<IDancerSynced>())
        {
            _dancerSynced.AddObserver(observer);
        }
    }
    private void Update()
    {
        _dancerSynced.UpdateAnimation(Time.deltaTime);
    }
}
