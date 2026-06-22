using UnityEngine;

[CreateAssetMenu(fileName = "DoorObjectSO", menuName = "Scriptable Objects/DoorObjectSO")]
public class DoorObjectSO : ScriptableObject
{

    public Transform newTransformPoint;
    public Transform currentTransformPoint;
    public string doorName;
    public bool canOpen;
}
