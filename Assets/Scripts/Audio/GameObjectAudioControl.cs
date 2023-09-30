using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Controls the audio for GameObjects
    /// </summary>
    public class GameObjectAudioControl : MonoBehaviour
    {
        [SerializeField]
        protected AudioSource m_audioSource;

        protected AudioControl m_audioControl;

        protected void Awake()
        {
            if (!m_audioSource)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its AudioSource component.");

            m_audioControl = new AudioControl(m_audioSource);
        }
    }
}
