using Player;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text for player's health
    /// </summary>
    public class PlayerHealthText : MenuText
    {
        [SerializeField]
        private PlayerBase m_playerBase;

        private void Awake()
        {
            base.Awake();

            if (!m_playerBase)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player base.");
            else
                m_playerBase.OnAttacked += UpdateText;
        }

        private void Start() => UpdateText();

        private void OnDisable() => m_playerBase.OnAttacked -= UpdateText;

        protected override void UpdateText() => m_text.text = m_playerBase.Health.ToString();
    }
}
