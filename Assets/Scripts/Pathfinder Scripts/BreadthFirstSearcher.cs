using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearcher : MonoBehaviour
{
    Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

    Queue<Waypoint> queue = new Queue<Waypoint>();

    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    public void BreadthFirstSearch(Dictionary<Vector2Int, Waypoint> grid, Waypoint startWaypoint, Waypoint endWaypoint, Waypoint searchCenter)
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && pathfinder.isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;

            HaltIfEndFound(searchCenter, endWaypoint);

            ExploreNeighbors(grid, searchCenter);
        }
    }

    void HaltIfEndFound(Waypoint searchCenter, Waypoint endWaypoint)
    {
        if (searchCenter == endWaypoint)
            pathfinder.isRunning = false;
    }

    void ExploreNeighbors(Dictionary<Vector2Int, Waypoint> grid, Waypoint searchCenter)
    {
        if (!pathfinder.isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPos = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighborPos))   //fixes errors about a non-existent coordinate
            {
                QueueNewNeighbors(grid, searchCenter, neighborPos);
            }
        }
    }

    void QueueNewNeighbors(Dictionary<Vector2Int, Waypoint>  grid, Waypoint searchCenter, Vector2Int adjacentPos)
    {
        Waypoint neighbor = grid[adjacentPos];
        
        if (!neighbor.isExplored && !queue.Contains(neighbor))
        {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }
}
