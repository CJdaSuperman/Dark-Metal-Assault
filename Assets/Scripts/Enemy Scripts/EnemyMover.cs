using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    Pathfinder pathfinder;

    [SerializeField] float movementSpeed = 0.4f;

    public int hitPower;

    HealthHandler healthHandler;

    bool pathCompleted = false;

    [SerializeField] ParticleSystem goalParticlePrefab;    

    void Awake() => healthHandler = GetComponent<HealthHandler>();

    void Start() => MoveEnemy();

    void MoveEnemy()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();               
        StartCoroutine(FollowPath(path));        
    }    

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint block in path)
        {
            transform.position = block.transform.position;
            yield return new WaitForSeconds(movementSpeed);
        }

        pathCompleted = true;
        healthHandler.DestroyEnemy(goalParticlePrefab);
    } 

    public bool IsPathCompleted() { return pathCompleted; }
}
