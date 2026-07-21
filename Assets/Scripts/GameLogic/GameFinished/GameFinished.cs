using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public static GameFinished Instance {get; private set;}

    [SerializeField] private string finalScene;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }    

    public void ChangeToFinalScene()
    {
        SceneManager.LoadScene(finalScene);
    }
}
