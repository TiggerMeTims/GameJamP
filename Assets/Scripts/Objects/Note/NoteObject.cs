using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] private NoteScript noteScript;

    public int GetNoteType()
    {
        return noteScript.ScriptType;
    }
}
