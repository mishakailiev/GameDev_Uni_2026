using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatforms : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb.linearVelocity.y > 0 && playerRb.position.y > rb.position.y + 0.4)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                StartCoroutine(SelfDestruct());
            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}