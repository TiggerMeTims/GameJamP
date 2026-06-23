using UnityEngine;

public class KeycardInteractions : MonoBehaviour, IKeycardInterface
{

    [SerializeField] private KeyCardSO keycardObjectSO;

    public bool HasRedKeycard()
    {
        return keycardObjectSO.hasRedKeycard;
    }
    public bool HasBlueKeycard()
    {
        return keycardObjectSO.hasBlueKeycard;
    }
    public bool HasYellowKeycard()
    {
        return keycardObjectSO.hasYellowKeycard;
    }
    
    //for the time being, I am setting this up to pass a string till I think of a better way
    public bool GetKeycard(string keycardType)
    {
        if(keycardType == "RedKeycard")
        {
            return HasRedKeycard();
        }
        else if(keycardType == "BlueKeycard")
        {
            return HasBlueKeycard();
        }
        else if(keycardType == "YellowKeycard")
        {
            return HasYellowKeycard();
        }
        else
        {
            return false;
        }
    }
    public void SetKeycard(string keycardType)
    {
        if(keycardType == "RedKeycard")
        {
            keycardObjectSO.hasRedKeycard = true;
        }
        if(keycardType == "BlueKeycard")
        {
            keycardObjectSO.hasBlueKeycard = true;
        }
        if(keycardType == "YellowKeycard")
        {
            keycardObjectSO.hasYellowKeycard = true;
        }
    }
}