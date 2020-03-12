using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    [SerializeField] Text finalScoreText;

    void OnEnable() => finalScoreText.text = Player.score.ToString();            

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Player.score = 0;
    }

    public void Quit() => Application.Quit();
}
