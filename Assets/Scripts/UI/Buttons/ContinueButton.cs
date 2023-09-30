using UnityEngine;

namespace UI.Buttons
{
    /// <summary>
    /// The behavior for the continue button
    /// </summary>
    public class ContinueButton : MenuButton
    {
        [SerializeField]
        private SceneTransition m_sceneTransition;

        private void Awake()
        {
            base.Awake();

            if (!m_sceneTransition)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Scene Transition.");
        }

        private void Start() => m_button.onClick.AddListener(m_sceneTransition.LoadNextScene);
    }
}