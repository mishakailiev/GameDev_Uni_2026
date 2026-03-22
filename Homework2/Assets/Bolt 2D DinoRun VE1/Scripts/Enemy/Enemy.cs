using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerHealth health;
    public float speed = 3f; // Speed of the enemy
    private Vector3 enemy_starting_position; // Starting position of the enemy
    public Vector3 enemy_starting_rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_starting_position = transform.position; // Store the starting position of the enemy
        enemy_starting_rotation = transform.eulerAngles; // Store the starting rotation of the enemy
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map End"))
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            transform.position = enemy_starting_position; // Reset the enemy's position to its starting point
            transform.eulerAngles = enemy_starting_rotation;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(1); // Call the TakeDamage method on the player's health UI
        }
    }
}
