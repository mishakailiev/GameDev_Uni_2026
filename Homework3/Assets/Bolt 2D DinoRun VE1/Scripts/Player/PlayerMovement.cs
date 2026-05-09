using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 40f;

    private Rigidbody2D rigidBody;
    private float horizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        ResolveLookDirection();
    }

    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            Vector2 force = new Vector2 (horizontalInput * moveSpeed, 0);
            if (Abs(rigidBody.linearVelocity.x) < 10)
            {
                rigidBody.AddForce(force);
            }
        }
        else
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x * 0.95f, rigidBody.linearVelocity.y);
        }

    }

    void ResolveLookDirection()
    {
        if (Abs(rigidBody.linearVelocity.x) > 0.01f)
        {
            transform.localScale = new Vector3(Sign(rigidBody.linearVelocity.x), 1, 1);
        }
    }
}
