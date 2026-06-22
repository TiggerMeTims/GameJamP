using UnityEngine;


public class DoorInteractions : MonoBehaviour, IDoorInteractionParent
{
    private bool canInteract = true;
    private bool playerHasKeyCardObject = false;

    public bool IsDoorInteractable()
    {
        return canInteract;
    }

    public void MovePlayerToNewPosition(Transform playerNewLocation)
    {
        Debug.Log("Script has been called to move the player to a new location");
    }

    public bool PlayerHasKeyCard()
    {
        return playerHasKeyCardObject;
    }

    public bool PlayerCollectKeyCard()
    {
        return !playerHasKeyCardObject;
    }
}