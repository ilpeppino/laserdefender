using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Game Object     EnemySpawner (Inspector)
Configuration   Waves/Wave x 
Classes used    WaveConfig
*/

// This script spawns enemies in waves (mapped from Wave x). It can be configured on multiple waves

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Calls a coroutine that spawns enemies within an indexed wave
        
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        // Iterates per nr of enemies times defined in Wave x
        for (int enemyCount=0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            // Instantiates an enemy in the position of the first waypoint of the wave and no rotation
            // This automatically generates a new enemy, so we can remove it from the list of game objects

            // waveConfig.GetWaypoints()[0] gets the waypoints from wave 0
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWaypoints()[0].transform.position, 
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawnsPrefab());
        }
    }


}
