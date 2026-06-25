using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attacks : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator animation;
    [Space]
    [SerializeField]
    private GameObject attack_right;
    [SerializeField] 
    private GameObject attack_up;
    [SerializeField] 
    private GameObject attack_down;
    [Space]
    [SerializeField]
    private float knockbackForce = 40f;
    [SerializeField]
    public float knockbackDuration = 0.05f;
    [SerializeField]
    public float attackDuration = 0.3f;

    private bool canAttack = true;

    private Vector3 add_offset = new Vector3(1.18f, 0.42f, 0f);
    private CapsuleDirection2D capsuleDirection = CapsuleDirection2D.Horizontal;

    private Rigidbody2D rb;

    // Audio 
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip attackSound;

    void Start()
    {
        attack_right.SetActive(false);
        attack_up.SetActive(false);
        attack_down.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && canAttack)
        {
            Debug.Log("Attack down!");
            capsuleDirection = CapsuleDirection2D.Vertical;
            StartVisualAttackDown();
        }
        else if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && canAttack)
            {
                Debug.Log("Attack up!");
                capsuleDirection = CapsuleDirection2D.Vertical;
                StartVisualAttackUp();
            }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && canAttack)
            {
                Debug.Log("Attack normal!");
                capsuleDirection = CapsuleDirection2D.Horizontal;
                StartVisualAttackNormal();
            }   
        }
    }

    void InitializeNormalAttack()
    {
        audioSource.PlayOneShot(attackSound);

        Collider2D col = attack_right.GetComponent<Collider2D>();

        Collider2D[] hits = Physics2D.OverlapCapsuleAll(
            col.bounds.center,
            col.bounds.size + add_offset,
            capsuleDirection,
            0f
        );

        bool hitsth = false;
        MobHealth temp = null;

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy" || hit.tag == "Spike")
            {
                hitsth = true;
            }
            temp = hit.GetComponent<MobHealth>();
            if (temp != null)
            {
                temp.TakeDamage(GetComponent<AttackDamage>().GetDamage());
            }
        }

        if (hitsth)
            knockBackPlayer(attack_right, false, 1f);

    }
    void InitializeUpAttack()
    {
        audioSource.PlayOneShot(attackSound);

        Collider2D col = attack_up.GetComponent<Collider2D>();

        Collider2D[] hits = Physics2D.OverlapCapsuleAll(
            col.bounds.center,
            col.bounds.size + add_offset,
            capsuleDirection,
            0f
        );

        bool hitsth = false;
        MobHealth temp = null;

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy" || hit.tag == "Spike")
            {
                hitsth = true;
            }
            temp = hit.GetComponent<MobHealth>();
            if (temp != null)
            {
                temp.TakeDamage(GetComponent<AttackDamage>().GetDamage());
            }
        }

        if (hitsth)
            knockBackPlayer(attack_up, false, 1f);

    }
    void InitializeDownAttack()
    {
        audioSource.PlayOneShot(attackSound);

        Collider2D col = attack_down.GetComponent<Collider2D>();

        Collider2D[] hits = Physics2D.OverlapCapsuleAll(
            col.bounds.center,
            col.bounds.size + add_offset,
            capsuleDirection,
            0f
        );

        bool hitsth = false;
        MobHealth temp = null;

        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy" || hit.tag == "Spike")
            {
                hitsth = true;
            }
            temp = hit.GetComponent<MobHealth>();
            if (temp != null)
            {
                temp.TakeDamage(GetComponent<AttackDamage>().GetDamage());
            }
        }

        if (hitsth)
            knockBackPlayer(attack_down, false, 1f);

    }

    public void knockBackPlayer(GameObject attack_, bool attack_or_damaged, float knockbackForceMultiplier)
    {
        PlayerMoveRL moveScript = GetComponent<PlayerMoveRL>();

        float xDir = (rb.position.x > attack_.transform.position.x) ? 1f : -1f;
        float yDir = (rb.position.y > attack_.transform.position.y) ? 1f : -1f;
        if(attack_or_damaged == false)
        {
            if (capsuleDirection == CapsuleDirection2D.Vertical) 
            {
                xDir = 0f;
                knockbackForceMultiplier = 3.5f;
            }
            else
                yDir = 0f;
        }
        else
        {
            if(rb.position.y > attack_.transform.position.y + 0.15f)
            {
                yDir = 1f;
                knockbackForceMultiplier = 2.5f;
            }
            else
            {
                yDir = 0f;
            }
        }

        Vector2 direction = new Vector2(xDir, yDir).normalized;
        StartCoroutine(ApplyKnockback(moveScript, direction, knockbackForceMultiplier));
    }

    private IEnumerator ApplyKnockback(PlayerMoveRL move, Vector2 dir, float forceMultiplier)
    {
        //Debug.Log("KnockBackApplied");
        if (move != null)
            move.canMove = false;
        float temp = 0f;
        if (rb.linearVelocity.y > 0f || rb.linearVelocity.y < 0f)
            temp = knockbackForce * 0.6f;
        else
            temp = knockbackForce;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(dir * temp * forceMultiplier, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration); 

        if (move != null) move.canMove = true;
    }


    private void StartVisualAttackUp()
    {
        animation.SetBool("isAttackingUp", true);
        canAttack = false;
    }
    private void StopVisualAttackUp()
    {
        canAttack = true;
        animation.SetBool("isAttackingUp", false);
    }

    private void StartVisualAttackDown()
    {
        animation.SetBool("isAttackingDown", true);
        canAttack = false;
    }
    private void StopVisualAttackDown()
    {
        canAttack = true;
        animation.SetBool("isAttackingDown", false);
    }

    private void StartVisualAttackNormal()
    {
        animation.SetBool("isAttackingNormal", true);
        canAttack = false;
    }
    private void StopVisualAttackNormal()
    {
        canAttack = true;
        animation.SetBool("isAttackingNormal", false);
    }
}