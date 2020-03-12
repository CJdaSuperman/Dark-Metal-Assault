using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    [SerializeField] float secondsBetweenWaves = 30f;
    float countdown = 2f;   //time before first wave spawns
    [SerializeField] Text waveText;
    [SerializeField] Text waveTimerText;
    [SerializeField] float spawnWaveEarlyTimer;

    int waveIndex = 0;

    [HideInInspector] public static int currentAmountEnemies = 0;
    int enemiesToSpawnCount = 0;

    [SerializeField] AudioClip spawnAudio;

    GameManager gameManager;

    bool wavesDefeated = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        waveText.text = "WAVE 0/" + waves.Length;
    }

    void Update()
    {
        if (gameManager.IsGameOver())
        {
            enabled = false;
            return;
        }

        if (waveIndex != waves.Length)
        {
            if (countdown <= 0)
            {
                waveText.text = "WAVE " + (waveIndex + 1).ToString() + "/" + waves.Length;
                StartCoroutine(SpawnWave());
                countdown = secondsBetweenWaves;
                waveIndex++;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            waveTimerText.text = string.Format("{0:00.00}", countdown);
        }
        else
        {
            countdown = 0;
            waveTimerText.text = string.Format("{0:00.00}", 0f);

            if (currentAmountEnemies == 0 && enemiesToSpawnCount == waves[waveIndex - 1].numOfEnemies)
            {
                wavesDefeated = true;
                enabled = false;
            }

            return;
        }
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        enemiesToSpawnCount = 0;

        if (wave.hasSubWaves)
        {
            int subWaveIndex = 0;   //used for going through subWaves array

            int subWaveEnemyCount = 0;  //counter for subWave number of enemies

            while (enemiesToSpawnCount < wave.numOfEnemies)
            {
                while (subWaveIndex < wave.subWaves.Length)
                {
                    Wave.SubWave subWave = wave.subWaves[subWaveIndex];

                    while (subWaveEnemyCount < subWave.numOfEnemiesInSubWave)
                    {
                        SpawnEnemy(subWave.subWaveEnemy);
                        enemiesToSpawnCount++;
                        currentAmountEnemies++;
                        subWaveEnemyCount++;
                        yield return new WaitForSeconds(1f / wave.rate);    //seconds between each enemy in this part
                    }

                    //If not the last subWave, do the pause
                    if (subWaveIndex != wave.subWaves.Length - 1)
                        yield return new WaitForSeconds(subWave.countdownBetweenParts);

                    subWaveIndex++;
                    subWaveEnemyCount = 0;
                }
            }
        }
        else
        {
            while (enemiesToSpawnCount < wave.numOfEnemies)
            {
                SpawnEnemy(wave.initialEnemyType);
                enemiesToSpawnCount++;
                currentAmountEnemies++;
                yield return new WaitForSeconds(1f / wave.rate);    //seconds between enemies
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(spawnAudio);
        newEnemy.transform.parent = transform;
    }

    public void InitiateNextWave()
    {
        if (countdown <= spawnWaveEarlyTimer && waveIndex != waves.Length)
            countdown = 0;

        return;
    }

    public bool IsWavesDefeated() { return wavesDefeated; }    
}
