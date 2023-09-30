using Managers;

namespace UI.Texts
{
    /// <summary>
    /// The text for the final score
    /// </summary>
    public class FinalScoreText : MenuText
    {
        private void Awake() => base.Awake();
        
        private void OnEnable() => UpdateText();

        protected override void UpdateText() => m_text.text = GameManager.PlayerScore.ToString();
    }
}
