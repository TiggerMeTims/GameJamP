using Unity.VisualScripting;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioFile;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            audioSource.clip = audioFile;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
