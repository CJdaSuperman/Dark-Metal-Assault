using Managers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Texts
{
    /// <summary>
    /// Text control for the player's score
    /// </summary>
    public class ScoreText : MenuText
    {
        private void Awake() => base.Awake();

        private void OnEnable()
        {
            GameManager.OnScoreUpdated += UpdateText;
            UpdateText();
        }

        private void OnDisable()
        {
            GameManager.OnScoreUpdated -= UpdateText;
        }

        protected override void UpdateText() => m_text.text = GameManager.PlayerScore.ToString();
    }
}
