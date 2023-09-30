using EnemySystem;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// Text control for display of the wave timer
    /// </summary>
    public class WaveTimerText : MenuText
    {
        [SerializeField]
        private WaveSpawner m_waveSpawner;

        private void Awake()
        {
            base.Awake();

            if (!m_waveSpawner)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the wave spawner.");
        }

        private void Update() => UpdateText();

        protected override void UpdateText()
        {
            m_text.text = 
                string.Format("{0:00.00}", Mathf.Clamp(m_waveSpawner.WaveTimer, 0, Mathf.Infinity));
        }
    }
}