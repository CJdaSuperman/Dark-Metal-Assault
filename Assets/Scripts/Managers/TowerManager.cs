using Environment;
using Player;
using Player.Tower;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the instantiation and positioning of towers
    /// </summary>
    public class TowerManager : MonoBehaviour
    {
        [SerializeField]
        private Tower m_towerPrefab;

        [SerializeField]
        [Tooltip("How many points needed to increase tower limit by 1")]
        private int m_towerScore;

        [SerializeField]
        [Tooltip("How many towers the player starts with")]
        private int m_startingTowers;

        [SerializeField]
        [Tooltip("The max amount of towers that can be placed down")]
        private int m_towerLimit;

        private Queue<Tower> m_towerQueue = new Queue<Tower>();

        public int TowerCount { get => m_towerQueue.Count; }

        public event Action OnInstantiate;

        private void Awake()
        {
            if (!m_towerPrefab)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the tower prefab.");
        }

        public bool IsTowerLimitReached() => m_towerQueue.Count == GetTowersAvailable();

        public void ShowNextMoveableTower() => m_towerQueue.Peek().ShowLimitReachedIndicator();

        public void AddTower(Waypoint baseWaypoint)
        {
            if (m_towerQueue.Count < GetTowersAvailable())
                InstantiateTower(baseWaypoint);
            else
                MoveExistingTower(baseWaypoint);
        }

        private void InstantiateTower(Waypoint baseWaypoint)
        {
            Tower newTower = Instantiate(m_towerPrefab, baseWaypoint.transform.position, Quaternion.identity);

            newTower.Initialize(baseWaypoint);
            newTower.transform.parent = transform;

            m_towerQueue.Enqueue(newTower);

            OnInstantiate?.Invoke();
        }

        private void MoveExistingTower(Waypoint newBaseWaypoint)
        {
            Tower movingTower = m_towerQueue.Dequeue();
            movingTower.Move(newBaseWaypoint);
            m_towerQueue.Enqueue(movingTower);
        }

        private int GetTowersAvailable()
        {
            int currentAmountOfTowers = m_startingTowers + (GameManager.PlayerScore / m_towerScore);
            return currentAmountOfTowers <= m_towerLimit ? currentAmountOfTowers : m_towerLimit;
        }
    }
}
