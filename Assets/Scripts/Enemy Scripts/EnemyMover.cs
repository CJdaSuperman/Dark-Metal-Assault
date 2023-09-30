using Environment;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Handles the movement of an enemy
    /// </summary>
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField]
        private float m_movementSpeed;

        [SerializeField]
        private int m_hitPower;

        private Pathfinder m_pathfinder;

        private WaitForSeconds m_delay;

        public bool PathComplete { get; private set; } = false;

        public event Action OnPathCompleted;

        private void Awake() => m_delay = new WaitForSeconds(m_movementSpeed);

        public void SetPathfinder(Pathfinder pathfinder) => m_pathfinder = pathfinder;

        public void Move() => StartCoroutine(FollowPath());

        private IEnumerator FollowPath()
        {
            foreach (Waypoint block in m_pathfinder.Path)
            {
                transform.position = block.transform.position;
                yield return m_delay;
            }

            PathComplete = true;
            OnPathCompleted?.Invoke();
        }
    }
}
