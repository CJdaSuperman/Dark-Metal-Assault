using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// The structure for how waves of enemies are
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        /// <summary>
        /// The attribute for an enemy to spawn during a part of a wave
        /// </summary>
        [System.Serializable]
        private struct EnemyAttributes
        {
            [SerializeField]
            public EnemyType enemyType;

            [SerializeField]
            [Tooltip("The delay after spawning an enemy in seconds")]
            public float secondsAfterSpawn;
        }

        [SerializeField]
        private EnemyAttributes[] m_enemies;

        private int m_enemyToSpawn = 0;

        public bool WaveComplete { get => m_enemyToSpawn == m_enemies.Length; }

        public float SpawnEnemy()
        {
            EnemyAttributes spawnedEnemy = m_enemies[m_enemyToSpawn];

            EnemyObjectPool.SpawnEnemy(spawnedEnemy.enemyType);

            m_enemyToSpawn++;

            return spawnedEnemy.secondsAfterSpawn;
        }
    }
}
