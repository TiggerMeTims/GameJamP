using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string _toMainGame;
    [SerializeField] private AudioSource menuSource;
    [SerializeField] private AudioClip menuTheme;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayClipSounds.Instance.PlayAudio(menuSource, menuTheme, true);
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
