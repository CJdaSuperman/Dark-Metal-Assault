using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;

    void Start() => gameManager = FindObjectOfType<GameManager>();    

    public void Continue() => gameManager.TogglePauseMenu();    

    public void Retry()
    {
        gameManager.TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() => Application.Quit();
}
