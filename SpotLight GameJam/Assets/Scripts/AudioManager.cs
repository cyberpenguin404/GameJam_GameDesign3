using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _celebration;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.DownArrow)) 
        //{
        //    DancerIlluminated(); 
        //}
    }

    void DancerIlluminated()
    {
        _audioSource.PlayOneShot(_celebration);
    }

}
