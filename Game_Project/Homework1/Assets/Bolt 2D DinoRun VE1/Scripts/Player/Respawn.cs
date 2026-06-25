using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //respawns the player to starting location
        if (collision.gameObject.CompareTag("Map End"))
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            transform.position = spawnPoint;

        }
    }
}
