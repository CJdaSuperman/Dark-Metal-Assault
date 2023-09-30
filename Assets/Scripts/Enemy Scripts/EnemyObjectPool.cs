using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// The object pool for enemies
    /// </summary>
    public static class EnemyObjectPool
    {
        private static Dictionary<EnemyType, Queue<Enemy>> m_enemies;

        public static List<Enemy> Targets { get; private set; }

        public static void InitializePool(Transform waveSpawner, Pathfinder pathfinder)
        {
            m_enemies = new Dictionary<EnemyType, Queue<Enemy>>();
            Targets   = new List<Enemy>();

            Enemy[] enemies = waveSpawner.GetComponentsInChildren<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                EnemyType enemyType = enemy.Type;

                if (!m_enemies.ContainsKey(enemyType))
                    m_enemies[enemyType] = new Queue<Enemy>();

                enemy.SetPathfinder(pathfinder);
                m_enemies[enemyType].Enqueue(enemy);
            }
        }

        public static void SpawnEnemy(EnemyType type)
        {
            Enemy spawnedEnemy = m_enemies[type].Dequeue();
            spawnedEnemy.SetActive();
            Targets.Add(spawnedEnemy);
        }

        public static void Despawn(Enemy enemy)
        {
            m_enemies[enemy.Type].Enqueue(enemy);
            Targets.Remove(Targets.Find((e) => e == enemy));
        }
    }
}
