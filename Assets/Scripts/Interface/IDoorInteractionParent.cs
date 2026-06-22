using UnityEngine;

public interface IDoorInteractionParent
{
    public bool IsDoorInteractable();
    public void MovePlayerToNewPosition(Transform playerNewLocation);
    public bool PlayerHasKeyCard();
    public bool PlayerCollectKeyCard();
}
