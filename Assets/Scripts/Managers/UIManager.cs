using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Component for the GameObject to manage UI canvases
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_playerHUD;

        [SerializeField]
        private Canvas m_pauseMenu;

        [SerializeField]
        private Canvas m_gameOverMenu;

        [SerializeField]
        private Canvas m_gameWonMenu;

        private GameObject PlayerHUD    { get => m_playerHUD.gameObject; }
        private GameObject PauseMenu    { get => m_pauseMenu.gameObject; }
        private GameObject GameOverMenu { get => m_gameOverMenu.gameObject; }
        private GameObject GameWonMenu  { get => m_gameWonMenu.gameObject; }

        private void Awake()
        {
            if (!m_playerHUD)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player HUD.");

            if (!m_pauseMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the pause menu.");

            if (!m_gameOverMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Game Over menu.");

            if (!m_gameWonMenu)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Game Won menu.");
        }

        private void OnEnable()
        {
            GameManager.OnGameLost += GameLost;
            GameManager.OnGameWon  += GameWon;
        }

        private void Start()
        {
            PlayerHUD.SetActive(true);
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(false);
            GameWonMenu.SetActive(false);
        }

        private void Update()
        {
            if (InputManager.PauseGame())
                TogglePause();
        }

        private void OnDisable()
        {
            GameManager.OnGameLost -= GameLost;
            GameManager.OnGameWon  -= GameWon;
        }

        public void TogglePause()
        {
            bool pauseMenuActive = PauseMenu.activeInHierarchy;
            PauseMenu.SetActive(!pauseMenuActive);
            GameManager.EnableTimeScale(pauseMenuActive);
        }

        private void GameLost()
        {
            GameOverMenu.SetActive(true);
            PlayerHUD.SetActive(false);
        }

        private void GameWon()
        {
            GameWonMenu.SetActive(true);
            PlayerHUD.SetActive(false);
        }
    }
}
