using UnityEngine;
using UnityEngine.UI;

namespace EnemySystem
{
    /// <summary>
    /// Handles the health of an enemy
    /// </summary>
    public class EnemyHealthHandler : MonoBehaviour
    {
        [SerializeField]
        private Enemy m_enemy;

        [SerializeField]
        private float m_maxHitPoints;

        [SerializeField]
        private Image m_healthBar;

        private float m_currentHitPoints;

        private void Awake()
        {
            if (!m_enemy)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the enemy.");
            else
                m_enemy.OnHit += OnHit;

            if (!m_healthBar)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its health bar.");
        }

        private void OnEnable()
        {
            m_currentHitPoints = m_maxHitPoints;
            m_healthBar.fillAmount = 1f;
        }

        private void OnDestroy() => m_enemy.OnHit -= OnHit;

        private void OnHit()
        {
            m_currentHitPoints--;
            m_healthBar.fillAmount = m_currentHitPoints / m_maxHitPoints;

            if (m_currentHitPoints <= 0)
                m_enemy.Destroy();
        }
    }
}
