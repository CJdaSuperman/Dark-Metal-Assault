using Environment;
using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// Forms the shortest path to player base based upon a world grid
    /// </summary>
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Transform that contains all the waypoints on the grid")]
        private WaypointsManager m_waypointsManager;

        [SerializeField]
        private Waypoint m_startWaypoint, m_endWaypoint;

        [SerializeField]
        private bool m_showPath;

        // The grid of waypoints with Vector2 coordinates as the key
        private Dictionary<Vector2Int, Waypoint> m_grid;

        public List<Waypoint> Path { get; private set; } = new List<Waypoint>();

        private void Awake()
        {
            if (!m_waypointsManager)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Waypoint manager.");

            if (!m_startWaypoint)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the waypoint that starts the path.");

            if (!m_endWaypoint)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the waypoint that ends the path.");
        }

        private void Start()
        {
            GetGrid();
            FindPath();
            CreatePath();
        }

        private void GetGrid()
        {
            GridBuilder blockLoader = new GridBuilder();
            m_grid = blockLoader.BuildGrid(m_waypointsManager);
        }

        private void FindPath()
        {
            BreadthFirstSearcher breadthFirstSearcher = new BreadthFirstSearcher();
            breadthFirstSearcher.BreadthFirstSearch(m_grid, m_startWaypoint, m_endWaypoint);
        }

        private void CreatePath()
        {
            AddWaypoint(m_endWaypoint);

            Waypoint previous = m_endWaypoint.ExploredFrom;

            while (previous != m_startWaypoint)
            {
                AddWaypoint(previous);
                previous = previous.ExploredFrom;
            }

            AddWaypoint(m_startWaypoint);

            Path.Reverse();
        }

        private void AddWaypoint(Waypoint waypoint)
        {
            waypoint.IsPlaceable = false;
            Path.Add(waypoint);

            if (m_showPath)
                waypoint.EnablePathIndicator();
        }
    }
}
