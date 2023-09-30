using Managers;
using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// The behavior for the Player's base
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField]
        private int m_maxHealth;

        public int Health { get; private set; }

        public event Action OnAttacked;

        private void Awake() => Health = m_maxHealth;

        private void OnTriggerEnter(Collider other)
        {
            Health--;

            if (Health <= 0)
                GameManager.LevelLost();
            else
                OnAttacked?.Invoke();
        }
    }
}
