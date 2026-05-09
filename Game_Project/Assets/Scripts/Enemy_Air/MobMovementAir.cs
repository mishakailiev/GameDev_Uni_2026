using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobMovementAir : MonoBehaviour
{
    [Header("GameAttributes")]
    [SerializeField]
    private Transform player;
    
    public bool canMove = true;

    [SerializeField]
    private BoxCollider2D patrolArea;
    private Vector2 targetPoint;

    [Header("Movement")]

    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float stopMovementOnHitDuration = 0.2f;
    [SerializeField]
    private bool facingRight = true;

    private bool isHit = false;

    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickNewTarget();
    }

    void FixedUpdate()
    {
        MoveToTarget();
    }

    void PickNewTarget()
    {
        if (IsPlayerInPatrolArea())
        {
            targetPoint = player.position;
        }
        else
        {
            targetPoint = GetRandomPointInBounds(patrolArea);
        }
    }

    void MoveToTarget()
    {
        Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
        if (!isHit)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = new Vector2(0.2f * rb.linearVelocity.x, 0.2f * rb.linearVelocity.y);
        }

        HandleFlip(direction);

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f || IsPlayerInPatrolArea())
        {
            PickNewTarget();
        }
    }

    void HandleFlip(Vector2 direction)
    {
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
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

    public IEnumerator ApplyStun()
    {
        Debug.Log("Mob Air is stunned.");
        isHit = true;
        yield return new WaitForSeconds(stopMovementOnHitDuration);
        isHit = false;
    }


    Vector2 GetRandomPointInBounds(BoxCollider2D box)
    {
        Bounds bounds = box.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    bool IsPlayerInPatrolArea()
    {
        return patrolArea.bounds.Contains(player.position);
    }
}
