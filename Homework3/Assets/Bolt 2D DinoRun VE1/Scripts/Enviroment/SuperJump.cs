using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            ApplySuperJump(rb);
        }
    }

    public void ApplySuperJump(Rigidbody2D collidedBody)
    {
        collidedBody.linearVelocity = new Vector2(0, 0);
        collidedBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
