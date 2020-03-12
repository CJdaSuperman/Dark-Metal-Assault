using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    void Awake() =>  audioSource = GetComponent<AudioSource>();

    public AudioSource GetAudioSource() { return audioSource; }

    public AudioClip GetEnemyHitSFX() { return enemyHitSFX; }

    public AudioClip GetEnemyDeathSFX() { return enemyDeathSFX; }
}
