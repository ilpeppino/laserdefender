using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Game Object     Prefabs/Enemy 
Configuration   
Classes used    WaveConfig
*/

// This script defines the path of one enemy. It is a component of Enemy

public class EnemyPathing : MonoBehaviour
    
{
    // I had put GameObject, but Transform is better since we need to get locations of the waypoints
    List<Transform> waypoints;
    WaveConfig waveConfig; // this is set in SetWaveConfig which is called from EnemySpawner when Enemy is created
    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        // the transform position is assigned to the object which this script is used from (that is Enemy), so that will be the Enemy position
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // This method is called from EnemySpawner and sets the waveConfig 
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        // "this" refers to the waveConfig defined at class level, and the other waveConfig is the parameter passed to this method
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {
            // Even if not expicitly declared, it will destroy the object which this script is used from (that is Enemy), so that will be the Enemy
            Destroy(gameObject);
        }
    }
}
