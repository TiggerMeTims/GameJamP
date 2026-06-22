using UnityEngine;

public class DoorObject : DoorInteractions
{
    [SerializeField] private DoorObjectSO doorObjectSO;
    public void Interaction(PlayerController player)
    {
        Debug.Log("Testing to see if this works");
    }

    public Transform GetNewPlayerPosition()
    {
        return doorObjectSO.newTransformPoint;
    }

    //This is created to make sure that all data is being passed accordingly
    public string TestFunctionPassing()
    {
        Debug.Log(doorObjectSO.newTransformPoint);
        return "Test";
    }

    public bool GetDoorCanOpen()
    {
        return doorObjectSO.canOpen;
    }
}