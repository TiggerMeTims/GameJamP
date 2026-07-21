using UnityEngine;
using TMPro;

public class DoorObject : DoorInteractions
{
    [SerializeField] private Transform doorOpenLocation;
    [SerializeField] private GameObject hunterAI;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text canvasText;
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