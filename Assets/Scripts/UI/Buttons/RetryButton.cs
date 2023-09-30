using Managers;
using UnityEngine;

namespace UI.Buttons
{
    /// <summary>
    /// The behavior for the retry button
    /// </summary>
    public class RetryButton : MenuButton
    {
        [SerializeField]
        private SceneTransition m_sceneTransition;

        private void Awake()
        {
            base.Awake();

            if (!m_sceneTransition)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Scene Transition.");
        }

        private void Start() => m_button.onClick.AddListener(Reload);

        private void Reload() => m_sceneTransition.ReloadScene();
    }
}