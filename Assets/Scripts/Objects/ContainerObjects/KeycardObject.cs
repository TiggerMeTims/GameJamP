using UnityEngine;

public class KeycardObject : KeycardInteractions
{

    public void Interaction()
    {
        Debug.Log("Testing connection to this script");
    }

    public string GetKeycardType()
    {
        Destroy(gameObject);
        return GetKeycard();
    }

    public void RemoveObject()
    {
        Destroy(this);
    }
}
