using UnityEngine;

public class PlayerTakeDMG : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isTakingDMG;
    private GameObject parent_player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isTakingDMG = false;
        parent_player = transform.parent.gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "Spike")
        {
            isTakingDMG = true;
            Debug.Log("Player hit from Collision!" + tag);
            PlayerHealth health = parent_player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                parent_player.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<EnemyDMG>().GetDamage());
            }
            Attacks do_knockback = parent_player.GetComponent<Attacks>();
            do_knockback.knockBackPlayer(collision.gameObject, true, 1f);
        }

        if(tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "Spike")
        {
            isTakingDMG = true;
            Debug.Log("Player hit from Collision!" + tag);
            PlayerHealth health = parent_player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                parent_player.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<EnemyDMG>().GetDamage());
            }
            Attacks do_knockback = parent_player.GetComponent<Attacks>();
            do_knockback.knockBackPlayer(collision.gameObject, true, 1f);
        }

        if (tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (parent_player.GetComponent<PlayerController>().canTakeDashDMG && tag == "EnemyBullet")
        {
            isTakingDMG = true;
            Debug.Log("Player hit from Trigger!" + tag);
            PlayerHealth health = parent_player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                parent_player.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<EnemyDMG>().GetDamage());
            }
            Attacks do_knockback = parent_player.GetComponent<Attacks>();
            do_knockback.knockBackPlayer(collision.gameObject, true, 0.6f);
        }

        if (tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (parent_player.GetComponent<PlayerController>().canTakeDashDMG && tag == "EnemyBullet")
        {
            isTakingDMG = true;
            Debug.Log("Player hit from Trigger!" + tag);
            PlayerHealth health = parent_player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                parent_player.GetComponent<PlayerHealth>().TakeDamage(collision.gameObject.GetComponent<EnemyDMG>().GetDamage());
            }
            Attacks do_knockback = parent_player.GetComponent<Attacks>();
            do_knockback.knockBackPlayer(collision.gameObject, true, 0.6f);
        }

        if (tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

}
