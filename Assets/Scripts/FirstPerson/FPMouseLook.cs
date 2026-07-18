using UnityEngine;
using UnityEngine.AI;

public class FPMouseLook : MonoBehaviour
{

    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 5f;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseInput = gameInput.Instance.GetLookVector();

        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mousey = mouseInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
