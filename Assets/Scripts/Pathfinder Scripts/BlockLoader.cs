using System.Collections.Generic;
using UnityEngine;

public class BlockLoader : MonoBehaviour
{
    public Dictionary<Vector2Int, Waypoint> LoadBlocks()
    {
        Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            //if the grid already has a key with the same grid position, skip adding that key
            if (grid.ContainsKey(waypoint.GetGridPos()))
            {
                //Used for Scene editing, to notify developer that there's multiple objects in one position
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }

        return grid;
    }
}
