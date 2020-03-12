using UnityEngine;

[System.Serializable]
public class Wave
{
    public bool hasSubWaves = false;

    [Tooltip("Prefab of first enemies to spawn in wave")]
    public GameObject initialEnemyType;

    public int numOfEnemies;

    [Tooltip("Enemies per sec")] public float rate;
    
    public SubWave[] subWaves;

    [System.Serializable]
    public class SubWave
    {        
        [Tooltip("If you want wave to have 1 enemy type, place same enemy prefab here")]
        public GameObject subWaveEnemy;

        [Tooltip("Number of enemies to spawn in this spawn wave, can't be more than numOfEnemies")]
        public int numOfEnemiesInSubWave = 0;

        [Tooltip("How long of a pause between each subwave in sec, if last part leave as zero")] public float countdownBetweenParts = 1f;        
    }    
}
