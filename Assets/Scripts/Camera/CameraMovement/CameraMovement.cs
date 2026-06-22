using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private static float cameraPosLag = 5.0f;

    private void LateUpdate()
    {
        transform.position = new(playerTransform.position.x, transform.position.y, playerTransform.position.z - cameraPosLag);
    }
}
