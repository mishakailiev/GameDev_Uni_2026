using UnityEngine;

public class MobAttackAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animation;

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Player" && animation != null)
        {
            animation.SetTrigger("Attack");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player" && animation != null)
        {
            animation.SetTrigger("Attack");
        }
    }
}
