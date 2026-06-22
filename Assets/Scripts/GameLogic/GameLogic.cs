using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject hiddenObject;

    void Start()
    {
        hiddenObject.SetActive(false);
        Debug.Log("We have hidden the game object");
    }
}
