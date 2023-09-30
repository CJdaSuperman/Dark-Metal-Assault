using Managers;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Spawns waves of different enemies
    /// </summary>
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private Pathfinder m_pathfinder;

        [SerializeField]
        private Wave[] m_waves;

        [SerializeField]
        [Tooltip("The countdown, in seconds, before waves begin spawning ")]
        private float m_countdownDuration;

        [SerializeField]
        private float m_secondsBetweenWaves;

        [SerializeField]
        [Tooltip("How many seconds left between waves can the player initiate the next wave")]
        private float m_waveInitiationCooldown;

        private int m_waveCounter = -1;

        private WaitForSeconds m_delayBetweenWaves;

        public float WaveTimer { get; private set; }

        public event Action OnWaveUpdated;

        private void Awake()
        {
            if (!m_pathfinder)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the pathfinder.");

            EnemyObjectPool.InitializePool(transform, m_pathfinder);
            m_delayBetweenWaves = new WaitForSeconds(m_secondsBetweenWaves);
            WaveTimer = m_countdownDuration;
        }

        private void Update()
        {
            if (GameManager.IsGameOver && enabled)
            {
                enabled = false;
                return;
            }

            if (m_waveCounter >= m_waves.Length && EnemyObjectPool.Targets.Count == 0)
            {
                GameManager.LevelWon();
                enabled = false;
                return;
            }

            if (WaveTimer <= 0 && m_waveCounter < m_waves.Length)
                StartCoroutine(Spawn());

            if (InputManager.NextWave() && WaveTimer < m_waveInitiationCooldown)
            {
                StopCoroutine(Spawn());
                StartCoroutine(Spawn());
            }

            WaveTimer -= Time.deltaTime;
        }

        public string WavesToString() => $"{m_waveCounter + 1}/{m_waves.Length}";

        private IEnumerator Spawn()
        {
            m_waveCounter++;

            if (m_waveCounter < m_waves.Length)
            {
                OnWaveUpdated?.Invoke();

                Wave wave = m_waves[m_waveCounter];

                WaveTimer = m_secondsBetweenWaves;

                while (!wave.WaveComplete)
                    yield return new WaitForSeconds(wave.SpawnEnemy());

                yield return m_delayBetweenWaves;
            }
        }
    }
}
