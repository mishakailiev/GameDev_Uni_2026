using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobMovement : MonoBehaviour
{
    [Header("GameAttributes")]

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Animator animation;

    public bool canMove = true;
    [Header("Movement")]

    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float stopMovementOnHitDuration = 0.2f;
    [SerializeField]
    private Transform positionA;
    [SerializeField]
    private Transform positionB;

    private bool isHit = false;
    private float moveSpeedWhenChasing;
    private float moveSpeedWhenPatrolling;

    private Rigidbody2D rb;
    [SerializeField]
    private bool facingRight = true;
    private int direction;

    [SerializeField]
    private float patrolAreaRadius;
    void Start()
    {
        moveSpeedWhenChasing = moveSpeed * 1.5f;
        moveSpeedWhenPatrolling = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        if (facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    void FixedUpdate()
    {
        if (GetComponent<MobHealthGround>().IsDead())
            return;
        if (canMove && !isHit)
        {
            Move();
        }
        else
        {
            rb.linearVelocity = new Vector2(0.2f * rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }

    private void Move()
    {
        float targetDir;

        if (IsPlayerInPatrolArea())
        {
            targetDir = Mathf.Sign(player.position.x - transform.position.x);
            direction = (int)targetDir;
            animation.SetBool("isPlayerNear", true);
            moveSpeed = moveSpeedWhenChasing;
        }
        else
        {
            if (positionA == null || positionB == null)
            {
                return;
            }
            if (Vector2.Distance(transform.position, positionB.position) < 1.5f && direction == 1)
            {
                direction = -1;
            }
            else if (Vector2.Distance(transform.position, positionA.position) < 1.5f && direction == -1)
            {
                direction = 1;
            }

            targetDir = direction;
            animation.SetBool("isPlayerNear", false);
            moveSpeed = moveSpeedWhenPatrolling;
        }

        rb.linearVelocity = new Vector2(moveSpeed * targetDir, rb.linearVelocity.y);

        HandleFlip(new Vector2(targetDir, 0));
    }

    void HandleFlip(Vector2 direction_)
    {
        if (direction_.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction_.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = rb.transform.localScale;
        scale.x *= -1;
        rb.transform.localScale = scale;
    }

    bool IsPlayerInPatrolArea()
    {
        return (Vector2.Distance(transform.position, player.position) < patrolAreaRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, patrolAreaRadius);
    }

    public IEnumerator ApplyStun()
    {
        Debug.Log("Mob Ground is stunned.");
        isHit = true;
        yield return new WaitForSeconds(stopMovementOnHitDuration);
        isHit = false;
    }

}
