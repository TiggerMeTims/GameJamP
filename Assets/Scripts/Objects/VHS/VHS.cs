using TMPro;
using UnityEngine;

public class VHS : MonoBehaviour
{
    private static string __ERRORMESSAGE__ = "VHS required to view scene \n\n Press E to Continue";

    public void HandleInteraction(GameObject canvas, TMP_Text canvasText)
    {
        HandleCanvas.Instance.HandleCanvasInteraction(canvas, canvasText, __ERRORMESSAGE__);
    }
}
