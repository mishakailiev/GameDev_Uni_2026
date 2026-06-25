using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Mathf;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rigidBody;

    private bool jump = false;
    private bool doubleJump = false;
    bool isGrounded = false;
    public bool Jump
    {
        get => jump;
        set => jump = value;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))    
        {
            //Debug.Log("Should Jump");
            jump = true;
        }
    }

    void JumpAction()
    {
        Vector2 jumpVelocity = rigidBody.linearVelocity;
        jumpVelocity.y = jumpForce;
        rigidBody.linearVelocity = jumpVelocity;
    }

    void FixedUpdate()
    {
        if (jump)
        {
            if (isGrounded)
            {
                //Debug.Log("Jump");
                JumpAction();
            }
            else
            {
                if (!doubleJump)
                {
                    JumpAction();
                    doubleJump = true;
                }
            }
            jump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
