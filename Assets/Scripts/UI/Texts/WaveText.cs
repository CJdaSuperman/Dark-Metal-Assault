using EnemySystem;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// Text control for display the enemy waves
    /// </summary>
    public class WaveText : MenuText
    {
        [SerializeField]
        private WaveSpawner m_waveSpawner;

        private void Awake()
        {
            base.Awake();

            if (!m_waveSpawner)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the wave spawner.");
            else
                m_waveSpawner.OnWaveUpdated += UpdateText;
        }

        private void OnDisable() => m_waveSpawner.OnWaveUpdated -= UpdateText;

        protected override void UpdateText() => m_text.text = $"WAVE {m_waveSpawner.WavesToString()}";
    }
}