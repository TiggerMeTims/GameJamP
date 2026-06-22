using UnityEngine;

public interface IKeycardInterface
{
    public bool HasRedKeycard();
    public bool HasBlueKeycard();
    public bool HasYellowKeycard();
    //for the time being, I am setting this up to pass a string till I think of a better way
    public bool GetKeycard(string keycardType);
    public void SetKeycard(string keycardType);
}
