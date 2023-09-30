using Player.Tower;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Handles the audio for the towers
    /// </summary>
    public class TowerAudioControl : GameObjectAudioControl
    {
        [SerializeField]
        private Tower m_tower;

        [SerializeField]
        private AudioClip m_laserSFX;

        private void Awake()
        {
            base.Awake();

            if (!m_tower)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its Tower.");
            else
                m_tower.OnLaserFired += OnLaserFired;

            if (!m_laserSFX)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the SFX for the laser.");
        }

        private void OnDisable() => m_tower.OnLaserFired -= OnLaserFired;

        private void OnLaserFired() => m_audioControl.Play(m_laserSFX, oneShot: false);
    }
}