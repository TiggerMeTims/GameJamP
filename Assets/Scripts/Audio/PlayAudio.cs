using Unity.VisualScripting;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip audioFile;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            PlayAudioFile();
        }
    }

    public void PlayAudioFile()
    {
        audioSource.clip = audioFile;
        audioSource.loop = true;
        audioSource.Play();
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public void StopAudioSource()
    {
        audioSource.Stop();
    }

}
