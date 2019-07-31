using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bullet;
    public Transform firePoint;
    public Rigidbody2D rb;
    public int health = 3;
    public float playerRange = 8f;
    public float moveSpeed = 1;
    public bool shooter;
    public float shooterRange = 5f;
    public float fireRate = 1f;

    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            // Enemy movement controller
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            rb.velocity = playerDirection.normalized * moveSpeed;

            // Enemy shot controller
            if (shooter && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shooterRange)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
