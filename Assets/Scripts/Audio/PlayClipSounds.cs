using UnityEngine;

public class PlayClipSounds : MonoBehaviour
{
    public static PlayClipSounds Instance {get; private set;}


    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayAudio(AudioSource controller, AudioClip audioClip, bool isLoop)
    {
        if(!IsAudioAttached(controller, audioClip) || !controller.isPlaying)
        {
            controller.clip = audioClip;
            controller.Play();
            controller.loop = isLoop;
        }
    }

    public void StopAudio(AudioSource controller)
    {
        controller.Stop();
    }

    private bool IsAudioAttached(AudioSource controller, AudioClip audioClip)
    {
        return controller.clip == audioClip;
    }
}
