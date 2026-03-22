using UnityEngine;
using static UnityEngine.Mathf;

public class AmmoMoveKill : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 40f;
    private Rigidbody2D rigidBody;
    public GameObject player;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Abs(transform.position.x) > 20)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (rigidBody == null) return;

        float direction = Sign(rigidBody.linearVelocity.x);
        Vector2 move = new Vector2(moveSpeed * direction, 0);
        rigidBody.AddForce(move);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<PlayerHealth>().TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}