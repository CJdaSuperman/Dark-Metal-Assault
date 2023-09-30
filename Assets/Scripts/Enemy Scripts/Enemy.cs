using Managers;
using Pathfinding;
using System;
using UnityEngine;

namespace EnemySystem
{
    /// <summary>
    /// Defines the behavior for Enemy GameObjects
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyType m_enemyType;

        [SerializeField]
        private int m_scorePoints;

        [SerializeField]
        private EnemyMover m_mover;

        [SerializeField]
        private ParticleSystem m_hitParticles;

        [SerializeField]
        private ParticleSystem m_deathFXPrefab;

        [SerializeField]
        private ParticleSystem m_goalParticlePrefab;

        public bool Active { get; private set; }

        public EnemyType Type { get => m_enemyType; }

        public event Action OnSpawned;
        public event Action OnHit;
        public event Action OnDeath;

        private void Awake()
        {
            if (!m_mover)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its mover.");
            else
                m_mover.OnPathCompleted += Destroy;

            if (!m_hitParticles)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its hit particles.");

            if (!m_deathFXPrefab)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its death particles prefab.");

            if (!m_goalParticlePrefab)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its goal particles prefab.");

            Active = false;
        }

        private void OnEnable()
        {
            if (Active)
            {
                m_mover.Move();
                OnSpawned?.Invoke();
            }
        }

        private void Start() => gameObject.SetActive(Active);

        private void OnParticleCollision(GameObject other)
        {
            m_hitParticles.Play();
            OnHit?.Invoke();
        }

        private void OnDisable()
        {
            if (Active)
            {
                Active = false;
                transform.position = Vector3.zero;
                EnemyObjectPool.Despawn(this);
                OnDeath?.Invoke();
            }
        }

        private void OnDestroy() => m_mover.OnPathCompleted -= Destroy;

        public void SetActive()
        {
            Active = true;
            gameObject.SetActive(Active);
        }

        public void SetPathfinder(Pathfinder pathfinder) => m_mover.SetPathfinder(pathfinder);

        public void Destroy()
        {
            bool pathComplete = m_mover.PathComplete;

            ParticleSystem vfx = Instantiate(pathComplete ? m_goalParticlePrefab : m_deathFXPrefab,
                                             transform.position,
                                             Quaternion.identity);
            Destroy(vfx.gameObject, vfx.main.duration);

            gameObject.SetActive(false);

            if (!pathComplete)
                GameManager.UpdateScore(m_scorePoints);
        }
    }
}
