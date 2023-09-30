using Environment;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// The Breadth First Search algorithm for finding shortest path to player base
    /// </summary>
    public class BreadthFirstSearcher
    {
        private readonly Vector2Int[] Directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        private Queue<Waypoint> m_traversalQueue = new Queue<Waypoint>();

        public void BreadthFirstSearch(Dictionary<Vector2Int, Waypoint> grid, Waypoint startWaypoint, Waypoint endWaypoint)
        {
            Waypoint searchCenter;
            
            m_traversalQueue.Enqueue(startWaypoint);

            while (m_traversalQueue.Count > 0)
            {
                searchCenter = m_traversalQueue.Dequeue();
                searchCenter.IsExplored = true;

                if (searchCenter == endWaypoint)
                    break;

                ExploreNeighbors(grid, searchCenter);
            }
        }

        private void ExploreNeighbors(Dictionary<Vector2Int, Waypoint> grid, Waypoint searchCenter)
        {
            foreach (Vector2Int direction in Directions)
            {
                Vector2Int neighborPos = searchCenter.GetGridPos() + direction;

                if (grid.ContainsKey(neighborPos))
                    QueueNewNeighbors(grid, searchCenter, neighborPos);
            }
        }

        private void QueueNewNeighbors(Dictionary<Vector2Int, Waypoint> grid, Waypoint searchCenter, Vector2Int adjacentPos)
        {
            Waypoint neighbor = grid[adjacentPos];

            if (!neighbor.IsExplored && !m_traversalQueue.Contains(neighbor))
            {
                m_traversalQueue.Enqueue(neighbor);
                neighbor.ExploredFrom = searchCenter;
            }
        }
    }
}
