using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance {get; private set;}

    [SerializeField] private GameObject gameOverUI;

    private bool gameOver = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Lots");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if(gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void GameOver()
    {
        if(gameOver) return;

        gameOver = true;

        if(gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
