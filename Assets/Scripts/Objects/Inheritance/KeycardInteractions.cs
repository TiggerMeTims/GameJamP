using UnityEngine;

public class KeycardInteractions : MonoBehaviour, IKeycardInterface
{

    [SerializeField] private KeyCardCollectableSO keycardCollectableObjectSO;

   
    
    //for the time being, I am setting this up to pass a string till I think of a better way
    public string GetKeycard()
    {
        return keycardCollectableObjectSO.keycardName;
    }
}