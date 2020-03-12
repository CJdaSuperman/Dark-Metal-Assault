using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start() => Invoke(nameof(LoadFirstLevel), 5f);

    void LoadFirstLevel() => SceneManager.LoadScene(1);
}
