using UnityEngine;

public class DoorObject : DoorInteractions
{
    [SerializeField] private Transform doorOpenLocation; 
    //private Transform doorOffset = new Vector3 (0, 0, 1);
    public void Interaction(PlayerController player)
    {
        Debug.Log("Testing to see if this works");
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

    public Transform DoorOpenLocation()
    {
        return doorOpenLocation;
    }
}