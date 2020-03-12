using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    bool gamePaused = false;

    Player player;
    WaveSpawner waveSpawner;

    [SerializeField] GameObject gameScreenUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject gameWonUI;

    void Start()
    {
        player = FindObjectOfType<Player>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (Input.GetKeyDown(KeyCode.T))
            waveSpawner.InitiateNextWave();

        if (Input.GetKeyDown(KeyCode.Tab))
            TogglePauseMenu();

        if (player.GetPlayerHealth() <= 0)
            EndGame();

        if (waveSpawner.IsWavesDefeated())
            LevelWon();
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
        waveSpawner.enabled = false;
    }

    public bool IsGameOver() { return gameEnded; }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        PauseGame();
    }

    void PauseGame()
    {
        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
            gamePaused = true;
        }            
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
        }            
    }

    public bool isGamePaused() { return gamePaused; }

    public void LevelWon()
    {
        gameEnded = true;
        gameScreenUI.SetActive(false);
        gameWonUI.SetActive(true);
    }
}
