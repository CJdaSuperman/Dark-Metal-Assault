using Managers;
using UnityEngine;

namespace UI.Texts
{
    /// <summary>
    /// The text for how many towers have been instantiated
    /// </summary>
    public class TowerCountText : MenuText
    {
        [SerializeField]
        private TowerManager m_towerManager;

        private void Awake()
        {
            base.Awake();

            if (!m_towerManager)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the tower factory.");
            else
                m_towerManager.OnInstantiate += UpdateText;

            m_text.text = "0";
        }

        private void OnDisable() => m_towerManager.OnInstantiate -= UpdateText;

        protected override void UpdateText() => m_text.text = m_towerManager.TowerCount.ToString();
    }
}
