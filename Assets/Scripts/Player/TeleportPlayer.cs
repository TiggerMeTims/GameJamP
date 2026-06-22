using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    //[SerializeField] private Transform PlayerLocation;
    [SerializeField] private Transform TargetLocation;

    private void _TeleportPlayerToLocation(Transform PlayerLocation)
    {
        PlayerLocation.position = TargetLocation.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        _TeleportPlayerToLocation(other.transform);
        Debug.Log(other.transform.position);
    }
}
