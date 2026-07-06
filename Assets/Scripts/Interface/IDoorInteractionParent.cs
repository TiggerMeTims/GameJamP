using UnityEngine;

public interface IDoorInteractionParent
{
    public bool IsDoorInteractable();
    public void MovePlayerToNewPosition(Transform playerNewLocation);
    public bool PlayerHasKeyCard();
    public bool PlayerCollectKeyCard();
    public string GetRequiredKeycard();
    public Transform GetCurrentTransformLocation();
    public Transform GetNewTransformLocation();
}
