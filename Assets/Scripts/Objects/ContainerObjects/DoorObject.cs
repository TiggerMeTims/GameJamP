using UnityEngine;

public class DoorObject : DoorInteractions
{
    [SerializeField] private Transform doorOpenLocation;

    public override void Interaction(PlayerController player)
    {
        Debug.Log("Door interaction called");

        if (player.IsDoorInteractable(this))
        {
            player.MovePlayerToNewPosition(doorOpenLocation);
        }
        else
        {
            Debug.Log("You have not collected the required keycard for this area");
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
}