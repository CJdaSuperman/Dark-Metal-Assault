using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the loading of scenes
/// </summary>
public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private float m_levelLoadDelay;

    private WaitForSeconds m_delay;

    private void Awake() => m_delay = new WaitForSeconds(m_levelLoadDelay);

    public void ReloadScene() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));

    public void LoadNextScene() => StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));

    private IEnumerator LoadScene(int sceneIndex)
    {
        GameManager.Reset();

        yield return m_delay;

        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(sceneIndex);
        else
            Application.Quit();
    }
}