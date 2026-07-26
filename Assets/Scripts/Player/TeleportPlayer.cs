using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    //public static TeleportPlayer Instance {get; private set;}
    [Header("Audio Files")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [Header("Hunter AI")]
    [SerializeField] private GameObject FinalHunter;
    [SerializeField] private GameObject HunterMovement;
    [Header("Player Objects")]
    //[SerializeField] private Transform PlayerLocation;
    [SerializeField] private Transform TargetLocation;
    /*
    private void Start()
    {
        Instance = this;
    }
    */

    private void _TeleportPlayerToLocation(Transform PlayerLocation)
    {
        PlayerLocation.position = TargetLocation.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        _TeleportPlayerToLocation(other.transform);

        //Change the audio from chace to ambiant, want to keep the tension when your caught
        if(audioSource != null)
        {
            if(PlayerController.Instance.hasRedKeycard)
                PlayClipSounds.Instance.PlayAudio(audioSource, audioClip, true);
        }

        //final hunter chases
        if(FinalHunter != null && FinalHunter.activeInHierarchy)
            MoveObject.Instance.MoveHunterPosition(FinalHunter, HunterMovement);
    }
}