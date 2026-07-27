using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance {get; private set;}
    [SerializeField] private Transform playerTransform;
    private static float cameraPosLag = 5.0f;
    public bool isFinalScene = false;
    private float changeValue = 0.004f;

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void LateUpdate()
    {
        transform.position = new(playerTransform.position.x, transform.position.y, playerTransform.position.z - cameraPosLag);
        if(isFinalScene)
            Camera.main.fieldOfView -= changeValue; 
    }
}
