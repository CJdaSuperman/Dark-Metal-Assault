using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;

    Transform targetEnemy;

    [HideInInspector] public Waypoint baseWaypoint;

    [SerializeField] AudioClip laserSFX;
    [SerializeField] AudioClip deathSFX;

    void Start()
    {
        //That way SetTargetEnmy isn't called every frame to improve on performance
        InvokeRepeating(nameof(SetTargetEnemy), 0f, 2f);
    }
    
    void Update()
    {
        if (targetEnemy)
        {
            LookAtEnemy(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            EmitLaser(false);
        }
    }

    void SetTargetEnemy()
    {
        EnemyMover[] sceneEnemies = FindObjectsOfType<EnemyMover>();

        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyMover enemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    Transform GetClosest(Transform closestEnemy, Transform currentEnemy)
    {
        var closestDistance = Vector3.Distance(transform.position, closestEnemy.position);
        var distanceToCurrent = Vector3.Distance(transform.position, currentEnemy.position);

        return distanceToCurrent < closestDistance ? currentEnemy : closestEnemy;
    }

    void LookAtEnemy(Transform enemyToTarget)
    {
        Vector3 direction = enemyToTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);  
        Vector3 rotation = 
            Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;  
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    void FireAtEnemy()
    {
        float distanceToEnemey = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        EmitLaser(distanceToEnemey <= attackRange);
    }

    //if the enemy is within range, turn on particles
    void EmitLaser(bool isActive)
    {
        var emissionMod = projectileParticle.emission;
        emissionMod.enabled = isActive;        
    }

    //For scene editing purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
