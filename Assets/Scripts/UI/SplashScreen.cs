using UnityEngine;

namespace UI
{
    /// <summary>
    /// Defines the behavior of the splash screen
    /// </summary>
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private SceneTransition m_sceneTransition;

        private void Awake()
        {
            if (!m_sceneTransition)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Scene Transition.");
        }

        void Start() => m_sceneTransition.LoadNextScene();
    }
}