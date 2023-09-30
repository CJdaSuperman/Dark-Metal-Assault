using Managers;
using UnityEngine;

namespace UI.Buttons
{
    /// <summary>
    /// The behavior for the resume button
    /// </summary>
    public class ResumeButton : MenuButton
    {
        [SerializeField]
        private UIManager m_uiManager;

        private void Awake()
        {
            base.Awake();

            if (!m_uiManager)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the UI manager.");
        }

        private void Start() => m_button.onClick.AddListener(m_uiManager.TogglePause);
    }
}