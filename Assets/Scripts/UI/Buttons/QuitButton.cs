using UnityEngine;

namespace UI.Buttons
{
    /// <summary>
    /// The behavior for the quit button
    /// </summary>
    public class QuitButton : MenuButton
    {
        private void Awake() => base.Awake();
        
        private void Start() => m_button.onClick.AddListener(() => Application.Quit());
    }
}
