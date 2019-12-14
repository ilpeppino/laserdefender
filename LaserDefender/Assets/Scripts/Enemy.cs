using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] float durationExplosion = 1f;
    [SerializeField] AudioClip sfx_EnemyExploding;
    [SerializeField] [Range(0,1)] float sfx_VolumeEnemyExploding = 0.7f;
    [SerializeField] AudioClip sfx_EnemyShooting;
    [SerializeField] [Range(0, 1)] float sfx_VolumeEnemyShooting = 0.2f;

    // We can map the Laser prefab into the Player object
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] GameObject starExplosionPrefab;
    
    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(sfx_EnemyShooting, Camera.main.transform.position, sfx_VolumeEnemyShooting);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        // damageDealer stores the DamageDealer component of the object which hits the enemy
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // Protecting agains null errors when damagedealer doesnt exist
        if (!damageDealer) { return; }
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
        
        GameObject explosion = Instantiate(starExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(explosion, durationExplosion);

        AudioSource.PlayClipAtPoint(sfx_EnemyExploding, Camera.main.transform.position, sfx_VolumeEnemyExploding);

    }
}
