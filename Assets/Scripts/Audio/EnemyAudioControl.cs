using EnemySystem;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Handles the audio for enemy GameObjects
    /// </summary>
    public class EnemyAudioControl : GameObjectAudioControl
    {
        [SerializeField]
        private Enemy m_enemy;

        [SerializeField]
        private AudioClip m_spawnSFX;

        [SerializeField]
        private AudioClip m_hitSFX;

        [SerializeField]
        private AudioClip m_deathSFX;

        private void Awake()
        {
            base.Awake();
            
            if (!m_enemy)
            {
                Debug.LogError($"{gameObject.name} doesn't have a reference to the enemy.");
            }
            else
            {
                m_enemy.OnSpawned += OnSpawn;
                m_enemy.OnHit     += OnHit;
                m_enemy.OnDeath   += OnDeath;
            }

            if (!m_hitSFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the SFX for spawning.");

            if (!m_hitSFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the SFX for getting hit.");

            if (!m_deathSFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the SFX for death.");
        }

        private void OnDestroy()
        {
            m_enemy.OnSpawned -= OnSpawn;
            m_enemy.OnHit     -= OnHit;
            m_enemy.OnDeath   -= OnDeath;
        }

        private void OnSpawn() => m_audioControl.Play(m_spawnSFX);

        private void OnHit() => m_audioControl.Play(m_hitSFX);

        private void OnDeath() => AudioSource.PlayClipAtPoint(m_deathSFX, Camera.main.transform.position);
    }
}