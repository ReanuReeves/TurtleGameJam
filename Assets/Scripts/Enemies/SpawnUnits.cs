using UnityEngine;

public class SpawnUnits : MonoBehaviour
{

    [SerializeField]
    GameObject[] unitPrefabs;

    [SerializeField]
    GameObject[] spawnPositions;

    bool spawnPause;
    [SerializeField] bool autoSpawn = true;

    [SerializeField] bool scaleOnSpawn = true;
    
    

    [SerializeField]
    float spawnCD = 1;
    [SerializeField]
    float spawnCDScalingFactor = 1;
    [SerializeField]
    float minSpawnCD = .5f;
    float timeUntilNextSpawn;

    [SerializeField]
    float waveSpawnCD = 30;
    [SerializeField]
    float waveCDScalingFactor = 1; 
     [SerializeField]
    float minWaveSpawnCD = 15;
    float timeUntilNextWaveSpawn;

    [SerializeField]
    float waveSize = 10;
    [SerializeField]
    float waveSizeScalingFactor = 1;
    [SerializeField]
    float maxWaveSize = 30;

    protected virtual void Start()
    {
        timeUntilNextSpawn = spawnCD;
        timeUntilNextWaveSpawn = waveSpawnCD;
    }

    protected virtual void Update()
    {
        if (!autoSpawn) return;
        if (spawnPause) return;

        timeUntilNextSpawn -= Time.deltaTime;
        timeUntilNextWaveSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn <= 0)
        {
            timeUntilNextSpawn = spawnCD;
            if (spawnCD > minSpawnCD && scaleOnSpawn) ScaleProperty(ref spawnCD, spawnCDScalingFactor, minSpawnCD);
            SpawnEnemy(-1, Random.Range(0, unitPrefabs.Length));
        }
        if (timeUntilNextWaveSpawn <= 0)
        {
            timeUntilNextWaveSpawn = waveSpawnCD;
            if (waveSpawnCD > minWaveSpawnCD && scaleOnSpawn) ScaleProperty(ref waveSpawnCD, waveCDScalingFactor, minWaveSpawnCD);
            if (waveSize < maxWaveSize && scaleOnSpawn) ScaleProperty(ref waveSize, waveSizeScalingFactor, maxWaveSize);
            SpawnEnemyWave((int)waveSize);
        }
    }

    protected void SpawnEnemyWave(int unitCount)
    {
        for (int i = 0; i < unitCount; i++)
        {
            SpawnEnemy(i);
        }
    }

    protected virtual void SpawnEnemy(int spawnPointIndex = -1, int enemyIndex = -1)
    {
        if (enemyIndex == -1) enemyIndex = Random.Range(0, unitPrefabs.Length);

        if (spawnPointIndex < 0) spawnPointIndex = Random.Range(0, spawnPositions.Length);
        GameObject temp = InstantiateEnemyAtNextSpawnpoint(spawnPointIndex, enemyIndex);
    }

    protected GameObject InstantiateEnemyAtNextSpawnpoint(int spawnPointIndex = -1, int enemyIndex = 0)
    {
        if (spawnPointIndex < 0) spawnPointIndex = UnityEngine.Random.Range(0, spawnPositions.Length);
        if (spawnPointIndex >= spawnPositions.Length) spawnPointIndex %= spawnPositions.Length;
        GameObject temp = (GameObject)Instantiate(unitPrefabs[enemyIndex], spawnPositions[spawnPointIndex].transform.position,
                                                                spawnPositions[spawnPointIndex].transform.rotation);
        return temp;
    }

    void ScaleProperty(ref float property, float scalingFactor, float finalValue)
    {
        property *= scalingFactor;
        if (scalingFactor < 1 && property < finalValue) property = finalValue;
        if (scalingFactor > 1 && property > finalValue) property = finalValue;
    }

    public void PauseSpawning()
    {
        spawnPause = true;
    }

    public void ResumeSpawning()
    {
        spawnPause = false;
    }
}
