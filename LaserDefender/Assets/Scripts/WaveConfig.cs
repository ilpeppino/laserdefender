using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Game object     Waves/Wave x
Configuration   Prefabs/Enemy
                Prefabs/Path x
                Time Between Spawns
                Spawn Random Factor
                Number of enemies
                Move Speed
*/

// This script defines and configures the wave of enemy (mapped from enemy prefab), waypoints (mapped from the path prefab) and other wave configuration

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{
    // enemyPrefab and pathPrefab are not assigned from inspector but in Wave x
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }


    public List<Transform> GetWaypoints() 
    {
        // Return waypoints from a path
        var waveWaypoints = new List<Transform>();

        // Path is parent of Waypoints
        // Therefore i am looping in the children of pathPrefab, accessing their Transform and adding them to the list
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints; 
    }
    public float GetTimeBetweenSpawnsPrefab() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }



}
