using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] float hitPoints;
    float currentHitPoints;

    [SerializeField] int scorePoints;

    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathFxPrefab;

    [SerializeField] Image healthBar;

    EnemyMover enemyMover;
    EnemyAudioController audioPlayer;

    Player player;

    void Awake()
    {
        enemyMover = GetComponent<EnemyMover>();
        audioPlayer = GetComponent<EnemyAudioController>();
    }

    void Start()
    {
        currentHitPoints = hitPoints;
        player = FindObjectOfType<Player>();
    }

    void OnParticleCollision(GameObject other)
    {
        audioPlayer.GetAudioSource().PlayOneShot(audioPlayer.GetEnemyHitSFX());
        currentHitPoints--;
        healthBar.fillAmount = currentHitPoints / hitPoints;
        hitParticlePrefab.Play();

        if (currentHitPoints <= 0)
            DestroyEnemy(deathFxPrefab);        
    }

    public void DestroyEnemy(ParticleSystem particle)
    {
        ParticleSystem vfx = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);

        AudioSource.PlayClipAtPoint(audioPlayer.GetEnemyDeathSFX(), Camera.main.transform.position);

        if(!enemyMover.IsPathCompleted())
            Player.score += scorePoints;

        WaveSpawner.currentAmountEnemies--;
        Destroy(gameObject);
    }
}
