using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera staticCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject hiddenObject;
    [SerializeField] private GameObject hiddenWall;

     private void OnTriggerEnter(Collider collider)
    {
        mainCamera.enabled = !mainCamera.enabled;
        staticCamera.enabled = !staticCamera.enabled;
        SetActiveItem(hiddenObject);
        SetActiveItem(hiddenWall);

    }

    private void SetActiveItem(GameObject targetObject)
    {
        if(targetObject.activeInHierarchy)
        {
            targetObject.SetActive(false);
            Debug.Log("We have hidden the game object");
        }
        else
        {
            targetObject.SetActive(true);
            Debug.Log("We have activated the game object");
        }
    }
}