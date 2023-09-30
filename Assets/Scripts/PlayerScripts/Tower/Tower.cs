using EnemySystem;
using Environment;
using Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Tower
{
    /// <summary>
    /// The defined behavior for a tower
    /// </summary>
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private TowerTurret m_turret;

        private TowerBase m_towerBase = new TowerBase();

        private Transform m_target;

        private Vector3 m_targetDirection = Vector3.zero;

        public event Action OnLaserFired;

        /// <summary>
        /// Visual representation of the attack range
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_turret.AttackRange * Waypoint.GridSize);
        }

        private void Awake()
        {
            if (!m_turret.PartToRotate)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the transform to rotate.");

            if (!m_turret.ProjectileParticle)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the projectile particles.");
        }

        private void OnEnable()
        {
            GameManager.OnGameWon  += StopLaser;
            GameManager.OnGameLost += StopLaser;
        }

        private void Start()
        {
            //That way SetTargetEnmy isn't called every frame to improve on performance
            InvokeRepeating(nameof(SetTarget), 0f, m_turret.SetTargetRate);
        }

        private void Update()
        {
            if (!GameManager.IsGamePaused && !GameManager.IsGameOver)
            {
                if (m_target)
                {
                    LookAtTarget();
                    FireAtTarget();
                }
                else
                {
                    StopLaser();
                }
            }
        }

        private void OnDisable()
        {
            GameManager.OnGameWon  -= StopLaser;
            GameManager.OnGameLost -= StopLaser;
        }

        public void Initialize(Waypoint baseWaypoint) => m_towerBase.Initialize(baseWaypoint);
        
        public void ShowLimitReachedIndicator() => m_towerBase.ShowLimitReached();

        public void Move(Waypoint newBaseWaypoint)
        {
            m_towerBase.Move(newBaseWaypoint);
            transform.position = newBaseWaypoint.transform.position;
            SetTarget();
        }

        private void SetTarget()
        {
            List<Enemy> targets = EnemyObjectPool.Targets;

            if (targets.Count == 0)
            {
                m_target = null;
                return;
            }

            Transform closestEnemy = targets[0].transform;

            foreach (Enemy enemy in targets)
                closestEnemy = GetClosestTarget(closestEnemy, enemy.transform);

            m_target = closestEnemy;
        }

        private void LookAtTarget()
        {
            Vector3 targetPosition = m_target.position;
            Vector3 towerPosition  = transform.position;
            m_targetDirection.Set(targetPosition.x - towerPosition.x,
                                  targetPosition.y - towerPosition.y,
                                  targetPosition.z - towerPosition.z);

            m_turret.LookAtTarget(Quaternion.LookRotation(m_targetDirection.normalized));
        }

        private void FireAtTarget()
        {
            float distanceToEnemey = Vector3.Distance(m_target.transform.position, transform.position);

            EmitLaser(distanceToEnemey <= m_turret.AttackRange);
        }

        private void EmitLaser(bool isActive)
        {
            m_turret.EmitLaser(isActive);

            if (isActive)
                OnLaserFired?.Invoke();
        }

        private void StopLaser() => EmitLaser(false);

        private Transform GetClosestTarget(Transform closestEnemy, Transform currentEnemy)
        {
            var closestDistance   = Vector3.Distance(transform.position, closestEnemy.position);
            var distanceToCurrent = Vector3.Distance(transform.position, currentEnemy.position);

            return distanceToCurrent < closestDistance ? currentEnemy : closestEnemy;
        }
    }
}
