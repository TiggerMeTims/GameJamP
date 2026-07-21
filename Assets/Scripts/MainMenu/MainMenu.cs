using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string _toMainGame;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_toMainGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
