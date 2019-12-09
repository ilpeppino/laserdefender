using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Calls a coroutine that spawns enemies within an indexed wave
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount=0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            // Instantiates an enemy in the position of the first waypoint of the wave and no rotation
            // This automatically generates a new enemy, so we can remove it from the list of game objects

            // waveConfig.GetWaypoints()[0] gets the waypoints from wave 0
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawnsPrefab());
        }
    }


}
