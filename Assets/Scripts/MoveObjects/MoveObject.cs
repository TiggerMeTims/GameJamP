using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public static MoveObject Instance {get; private set;}
    
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void MoveHunterPosition(GameObject hunter, GameObject newPosition)
    {
        Debug.Log("Hunter Move Call");
        hunter.transform.position = newPosition.transform.position;
    }
}
