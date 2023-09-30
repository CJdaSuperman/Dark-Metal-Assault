using Player;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Handles the audio for the player base
    /// </summary>
    public class PlayerBaseAudioControl : GameObjectAudioControl
    {
        [SerializeField]
        private PlayerBase m_playerBase;

        [SerializeField]
        private AudioClip m_hitSFX;

        private void Awake()
        {
            base.Awake();

            if (!m_playerBase)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player base.");
            else
                m_playerBase.OnAttacked += OnAttack;

            if (!m_hitSFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the SFX for getting hit.");
        }

        private void OnDisable() => m_playerBase.OnAttacked -= OnAttack;

        private void OnAttack() => m_audioControl.Play(m_hitSFX);
    }
}