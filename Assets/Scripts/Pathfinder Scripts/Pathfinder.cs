using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] public Waypoint startWaypoint, endWaypoint;

    Waypoint searchCenter;

    [SerializeField] public ParticleSystem pathIndicator;

    //The Vector2Int is going to be my key for a Waypoint obj
    public Dictionary<Vector2Int, Waypoint> grid;    
                
    List<Waypoint> path = new List<Waypoint>();

    public bool isRunning { get; set; } = true;

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0) //if path hasn't already been found, form it; if it has, return it
        {
            LoadBlocks();
            FindPath();
            CreatePath();           
        }

        return path;
    }

    void LoadBlocks()
    {
        BlockLoader blockLoader = GetComponent<BlockLoader>();
        grid = blockLoader.LoadBlocks();
    }
    
    void FindPath()
    {
        BreadthFirstSearcher breadthFirstSearcher = new BreadthFirstSearcher();
        breadthFirstSearcher.BreadthFirstSearch(grid, startWaypoint, endWaypoint, searchCenter);
    }

    void CreatePath()
    {
        AddWaypoint(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;

        while(previous != startWaypoint)
        {
            AddWaypoint(previous);
            previous = previous.exploredFrom;
        }

        AddWaypoint(startWaypoint);

        path.Reverse();        
    }    

    void AddWaypoint(Waypoint waypoint)
    {
        waypoint.isPlaceable = false;
        path.Add(waypoint);
    }
}
