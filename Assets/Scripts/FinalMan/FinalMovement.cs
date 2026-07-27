using UnityEngine;

public class FinalMovement : MonoBehaviour
{
    private float moveSpeed = 0.7f;
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * Time.deltaTime * moveSpeed;
    }
}
