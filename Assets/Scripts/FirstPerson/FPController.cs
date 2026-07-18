using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;

public class FPController : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 5f;

    private CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 input = gameInput.Instance.GetMovementVectorNormalized();

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        controller.Move(move * characterSpeed * Time.deltaTime);
    }
}
