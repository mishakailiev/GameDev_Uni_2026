using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private bool direction = false; // true = towards the PLAYER, false = Horisontal direction
    private Rigidbody2D rb;

    private Vector2 direction_vector;
    private float targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (direction)
        {
            direction_vector = (player.position - transform.position).normalized;
            targetRotation = Mathf.Atan2(direction_vector.y, direction_vector.x) * Mathf.Rad2Deg;
            rb.MoveRotation(targetRotation);
            rb.linearVelocity = direction_vector * speed;
        }
        else
        {
            rb.linearVelocity = new Vector2(speed, 0f);
        }
    }
}
