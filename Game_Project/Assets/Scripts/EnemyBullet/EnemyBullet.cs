using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private bool direction = false; // true = towards the PLAYER, false = Horisontal direction
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        if (direction)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = new Vector2(speed, 0f);
        }
    }
}
