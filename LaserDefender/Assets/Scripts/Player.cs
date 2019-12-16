using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Game object    : Prefab/Player
Configuration  : Laser
                 Move Speed
                 Padding
                 Projectile Speed
                 Projectile Firing Period
*/
public class Player : MonoBehaviour

{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 100;
    [SerializeField] AudioClip sfx_PlayerShooting;
    [SerializeField] [Range(0, 1)] float sfx_VolumePlayerShooting = 0.2f;
    [SerializeField] AudioClip sfx_PlayerExploding;
    [SerializeField] [Range(0, 1)] float sfx_VolumePlayerExploding = 0.7f;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] GameObject laserPrefab;

    Coroutine firingCoroutine;

    // We can map the Laser prefab into the Player object
    

    float xMin, yMin;
    float xMax, yMax;


    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
        
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        FireLaser();
    }


    private void FireLaser()
    {
        // GetButtonDown checks the Fire1 in the Project Settings -> Input
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        // Once FireContinuously is called, it will loop indefinitely until the GetButtonUp condition (which is checked every frame update) is satisfied
        while (true)
        {
            // Instantiate a laserPrefab in the current position of player with no rotation. laserPrefab is known as it has been mapped from the inspector via the SerializeField
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            AudioSource.PlayClipAtPoint(sfx_PlayerShooting, Camera.main.transform.position, sfx_VolumePlayerShooting);

            // Access the Rigidbody component of the laser
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // damageDealer stores the DamageDealer component of the object which hits the enemy
        Debug.Log("OnTriggerEnter2D called");
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // Protecting agains null errors when damagedealer doesnt exist
        if (!damageDealer) { return;  }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(sfx_PlayerExploding, Camera.main.transform.position, sfx_VolumePlayerExploding);
    }

    private void Move()
    {
        // This is connected to Edit -> Project Settings -> Input. In here you can define the specs for input devices (keys, mouse, joystick) to move sprites 
        // Saying Horizontal means that deltaX is calculated from the input device movement on the x-axis defined in Project Settings input as Horizontal (Vertical for y-axis)
        // Time.deltaTime calculates the time between frames (times the Update is called), to speed up or slow down use a constant value (p.e. moveSpeed)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Debug.Log(deltaX);

        // var is an undefined local variable, which takes the type from the assignment. P.e. deltaX will be a float because GetAxis is a float
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // The new position will be moving on x-axis but staying on y-axis
        transform.position = new Vector2(newXPos, newYPos);


    }

    // Define the boundaries of the game camera in world points
    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
