using Managers;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Defines the behavior of Waypoints
    /// </summary>
    public class Waypoint : MonoBehaviour
    {
        public const int GridSize = 10;

        [Header("Waypoint Indicators")]
        [SerializeField]
        private Transform m_placeTowerIndicator;

        [SerializeField]
        private Transform m_cantPlaceIndicator;

        [SerializeField]
        private Transform m_limitReachedIndicator;

        [SerializeField]
        private ParticleSystem m_pathIndicator;

        private TowerManager m_towerManager;
        private Vector2Int m_gridPos = Vector2Int.zero;

        public bool       IsExplored            { get; set; } = false;
        public Waypoint   ExploredFrom          { get; set; }
        public bool       IsPlaceable           { get; set; } = true;
        public GameObject LimitReachedIndicator { get => m_limitReachedIndicator.gameObject; }

        private GameObject PlaceTowerIndicator  { get => m_placeTowerIndicator.gameObject; }
        private GameObject CantPlaceIndicator   { get => m_cantPlaceIndicator.gameObject; }
        private GameObject PathIndicator        { get => m_pathIndicator.gameObject; }

        private void Awake()
        {
            if (!m_placeTowerIndicator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Pathfinder.");

            if (!m_cantPlaceIndicator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the path indicator particles.");

            if (!m_limitReachedIndicator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the path indicator particles.");

            if (!m_pathIndicator)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the path indicator particles.");

            m_gridPos.x = Mathf.RoundToInt(transform.position.x / GridSize);
            m_gridPos.y = Mathf.RoundToInt(transform.position.z / GridSize);
        }

        private void OnMouseOver()
        {
            if (!GameManager.IsGameOver && !GameManager.IsGamePaused)
            {
                if (IsPlaceable)
                {
                    if (m_towerManager.IsTowerLimitReached())
                    {
                        PlaceTowerIndicator.SetActive(false);
                        LimitReachedIndicator.SetActive(true);
                        m_towerManager.ShowNextMoveableTower();
                    }
                    else
                    {
                        PlaceTowerIndicator.SetActive(true);
                    }
                }
                else
                {
                    CantPlaceIndicator.SetActive(true);
                }
            }
        }

        private void OnMouseExit()
        {
            if (!GameManager.IsGameOver && !GameManager.IsGamePaused)
            {
                PlaceTowerIndicator.SetActive(false);
                CantPlaceIndicator.SetActive(false);
                LimitReachedIndicator.SetActive(false);
            }
        }

        private void OnMouseDown()
        {
            if (!GameManager.IsGameOver   && 
                !GameManager.IsGamePaused &&
                IsPlaceable)
            {
                m_towerManager.AddTower(this);
            }
        }

        public void Initialize(TowerManager towerManager) => m_towerManager = towerManager;

        public Vector2Int GetGridPos()
        {
            m_gridPos.x = Mathf.RoundToInt(transform.position.x / GridSize);
            m_gridPos.y = Mathf.RoundToInt(transform.position.z / GridSize);
            return m_gridPos;
        }

        public void EnablePathIndicator()
        {
            PathIndicator.SetActive(true);
            m_pathIndicator.Play();
        }
    }
}

