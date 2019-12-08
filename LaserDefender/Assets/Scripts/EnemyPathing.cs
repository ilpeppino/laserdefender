using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
    
{
    // I had put GameObject, but Transform is better since we need to get locations of the waypoints
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // the transform position is assigned to the object which this script is used from (that is Enemy), so that will be the Enemy position
        // transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;

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
