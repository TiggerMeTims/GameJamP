using UnityEngine;

public interface IDoorInteractionParent
{
    bool IsDoorInteractable();

    bool PlayerHasKeyCard();

    bool PlayerCollectKeyCard();

    string GetRequiredKeycard();

    Transform GetCurrentTransformLocation();

    Transform GetNewTransformLocation();
}