using Environment;
using System;
using UnityEngine;

namespace Player.Tower
{
    [Serializable]
    public class TowerTurret
    {
        [SerializeField]
        private float m_turnSpeed;

        [SerializeField]
        [Tooltip("The attack range of the tower measured in waypoints")]
        private float m_attackRange;

        [SerializeField]
        [Tooltip("The interval to set a target in seconds")]
        private float m_setTargetRate;

        [SerializeField]
        private Transform m_partToRotate;

        [SerializeField]
        private ParticleSystem m_projectileParticle;

        public float TurnSpeed                    { get => m_turnSpeed; }
        public float AttackRange                  { get => m_attackRange * Waypoint.GridSize; }
        public float SetTargetRate                { get => m_setTargetRate; }
        public Transform PartToRotate             { get => m_partToRotate; }
        public ParticleSystem ProjectileParticle  { get => m_projectileParticle; }

        public void LookAtTarget(Quaternion lookRotation)
        {
            Vector3 rotation =
                Quaternion.Lerp(m_partToRotate.rotation, lookRotation, Time.deltaTime * m_turnSpeed).eulerAngles;

            m_partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }

        public void EmitLaser(bool isActive)
        {
            var emissionMod = m_projectileParticle.emission;
            emissionMod.enabled = isActive;
        }
    }

    public class TowerBase
    {
        private Waypoint m_baseWaypoint;

        public void Initialize(Waypoint baseWaypoint)
        {
            baseWaypoint.IsPlaceable = false;
            m_baseWaypoint = baseWaypoint;
        }

        public void ShowLimitReached() => m_baseWaypoint.LimitReachedIndicator.SetActive(true);

        public void Move(Waypoint newBaseWaypoint)
        {
            m_baseWaypoint.IsPlaceable = true;
            m_baseWaypoint.LimitReachedIndicator.SetActive(false);

            newBaseWaypoint.IsPlaceable = false;
            m_baseWaypoint = newBaseWaypoint;
        }
    }
}
