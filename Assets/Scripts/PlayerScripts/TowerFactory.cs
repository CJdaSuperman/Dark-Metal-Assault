using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerQueue = new Queue<Tower>();
    [SerializeField] Text towerCountText;

    [SerializeField] Transform towersTransform;

    Player player;

    void Start() { player = FindObjectOfType<Player>(); }

    public bool IsTowerLimitReached() { return towerQueue.Count == player.GetTowersAvailable(); }

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towerQueue.Count < player.GetTowersAvailable())        
            InstantiateTower(baseWaypoint);        
        else
            MoveExistingTower(baseWaypoint);
    }    

    void InstantiateTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towersTransform;    //to manage placed towers in Heirarchy
        towerCountText.text = (towerQueue.Count + 1).ToString();

        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;

        towerQueue.Enqueue(newTower);
    }

    void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        Tower oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoint.isPlaceable = true;
        oldTower.baseWaypoint.limitReachedIndicator.gameObject.SetActive(false);

        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = newBaseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }

    public Tower GetTowerToMove() { return towerQueue.Peek(); }
}
