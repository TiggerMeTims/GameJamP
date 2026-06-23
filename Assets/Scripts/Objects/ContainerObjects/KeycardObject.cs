using UnityEngine;

public class KeycardObject : KeycardInteractions
{

    [SerializeField] private string keycardType;

    public void Interaction()
    {
        Debug.Log("Testing connection to this script");
    }

    public void HasKeycard()
    {
        if(!GetKeycard(keycardType))
        {
            SetKeycard(keycardType);
        }
        else
        {
            Debug.Log("Keycard already set");
        }
    }

    public bool CheckKeycardAvailable(string keycardType)
    {
        return GetKeycard(keycardType);
    }

    public string GetKeycardType()
    {
        return keycardType;
    }
}
