using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public static TeleportPlayer Instance {get; private set;}

    //[SerializeField] private Transform PlayerLocation;
    [SerializeField] private Transform TargetLocation;

    private void _TeleportPlayerToLocation(Transform PlayerLocation)
    {
        PlayerLocation.position = TargetLocation.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        _TeleportPlayerToLocation(other.transform);
    }
}