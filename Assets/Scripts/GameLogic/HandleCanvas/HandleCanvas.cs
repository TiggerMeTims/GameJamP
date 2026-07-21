using TMPro;
using UnityEngine;

public class HandleCanvas : MonoBehaviour
{
    public static HandleCanvas Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void HandleCanvasInteraction(GameObject canvas, TMP_Text canvasText, string errorMessage)
    {
        if(canvas == null)
            return;
        
        if(canvas.activeInHierarchy)
        {
            canvas.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            canvasText.text = errorMessage;
            canvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
