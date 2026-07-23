using UnityEngine;

public class KeycardObject : KeycardInteractions
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ejectSound;

    public void Interaction()
    {
        Debug.Log("Testing connection to this script");
    }

    public string GetKeycardType()
    {
        Destroy(gameObject);
        return GetKeycard();
    }

    public void RemoveObject()
    {
        Destroy(this);
    }

    public void PlayEjectSound()
    {
        if(PlayClipSounds.Instance != false)
            PlayClipSounds.Instance.PlayAudio(audioSource, ejectSound, false);
    }
}
