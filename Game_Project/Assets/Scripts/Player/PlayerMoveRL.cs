using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveRL : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator animation;

    public bool canMove = true;
    [Header("Movement")]

    [SerializeField]
    private float moveSpeed = 6f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight = true;

    private bool isTouchingWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (GetComponent<PlayerController>().isDashing) return;
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!canMove) return;

        if(isTouchingWall)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animation.SetBool("isRunning", false);
        }
        else if(moveInput != 0)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            animation.SetBool("isRunning", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.75f, rb.linearVelocity.y);
            animation.SetBool("isRunning", false);
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
