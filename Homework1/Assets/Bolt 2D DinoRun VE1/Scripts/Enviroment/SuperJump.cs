using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
