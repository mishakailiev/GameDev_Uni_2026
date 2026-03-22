using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f; // Speed at which the enemy moves towards the player
    public Vector3 startingposition;
    public Rigidbody2D enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingposition = transform.position; // Store the starting position of the enemy
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Move the enemy towards the player
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            if (0 < direction.y)
            {
                direction.y = 0;
            }
            enemy.linearVelocity = direction * speed; // Adjust speed as needed
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Move the enemy towards the player
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            if (0 < direction.y)
            {
                direction.y = 0;
            }
            enemy.linearVelocity = direction * speed; // Adjust speed as needed
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the enemy's movement when the player exits the trigger area
            enemy.linearVelocity = Vector3.zero;
        }
    }
}
