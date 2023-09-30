using Environment;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the waypoints in a level
    /// </summary>
    public class WaypointsManager : MonoBehaviour
    {
        [SerializeField]
        private TowerManager m_towerManager;

        public Waypoint[] Waypoints { get; private set; }

        private void Awake()
        {
            if (!m_towerManager)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Tower manager.");

            Waypoints = transform.GetComponentsInChildren<Waypoint>();
        }

        private void Start()
        {
            foreach (Waypoint wpt in Waypoints)
                wpt.Initialize(m_towerManager);
        }
    }
}
