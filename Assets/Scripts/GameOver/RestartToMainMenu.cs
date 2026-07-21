using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartToMainMenu : MonoBehaviour
{
    public string _toMainMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(_toMainMenu);
    }
}
