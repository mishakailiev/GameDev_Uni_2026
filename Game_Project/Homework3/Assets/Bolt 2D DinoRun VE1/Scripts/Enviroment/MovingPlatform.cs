using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float spanWidth;
    [SerializeField]
    private float speed;
    private float startingX;
    private bool isMovingRight;

    Rigidbody2D rb;

    void Start()
    {
        startingX = transform.position.x;
        isMovingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isMovingRight)
        {
            rb.linearVelocity = new Vector3(speed, 0, 0);
            if (transform.position.x > startingX + spanWidth)
            {
                isMovingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(-speed, 0, 0);
            if (transform.position.x < startingX - spanWidth)
            {
                isMovingRight = true;
            }
        }
    }
}
