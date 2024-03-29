﻿using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    /// <summary>
    /// Builds the grid of the world
    /// </summary>
    public class GridBuilder
    {
        public Dictionary<Vector2Int, Waypoint> BuildGrid(WaypointsManager waypointsManager)
        {
            Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

            foreach (Waypoint waypoint in waypointsManager.Waypoints)
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
}
