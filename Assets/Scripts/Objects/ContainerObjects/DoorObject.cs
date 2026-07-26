using UnityEngine;
using TMPro;

public class DoorObject : DoorInteractions
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [Header("Hunter and Canvas")]
    [SerializeField] private Transform doorOpenLocation;
    [SerializeField] private GameObject hunterAI;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text canvasText;
    [Header("Final Hunter AI")]
    [SerializeField] private GameObject finalHunter;
    [SerializeField] private GameObject finalHunterNewPosition;
    private static string errorMessage = "You have not collected the required keycard for this area \n\n Press E to Continue";
    private static string __REQUIREDKEYCARD__ = "YellowKeyCard";


    public override void Interaction(PlayerController player)
    {
        if (player.IsDoorInteractable(this))
        {
            DisableHunterAI();
            if(GetRequiredKeycard() == __REQUIREDKEYCARD__)
            {
                hunterAI.SetActive(true);
                Invoke(nameof(CallFinalScene), 10f);
                PlayerController.Instance.SetMoveSpeed(0f);
            }
            player.MovePlayerToNewPosition(doorOpenLocation);
            if(audioSource != null)
            {
                if(PlayerController.Instance.hasRedKeycard)
                    PlayClipSounds.Instance.PlayAudio(audioSource, audioClip, true);
            }
            if(finalHunter != null && finalHunter.activeInHierarchy)
            {
                Debug.Log("Door Call");
                MoveObject.Instance.MoveHunterPosition(finalHunter, finalHunterNewPosition);
            }
        }
        else
        {
            if(HandleCanvas.Instance != null)
                HandleCanvas.Instance.HandleCanvasInteraction(canvas, canvasText, errorMessage);
        }
    }

    public bool GetDoorCanOpen()
    {
        return doorObjectSO.canOpen;
    }

    public Transform DoorOpenLocation()
    {
        return doorOpenLocation;
    }

    private void DisableHunterAI()
    {
        if (hunterAI == null)
            return;

        hunterAI.SetActive(false);
    }

    private void CallFinalScene()
    {
        GameFinished.Instance.ChangeToFinalScene();
    }
}