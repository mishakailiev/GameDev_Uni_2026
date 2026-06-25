using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator animation;
    private Rigidbody2D rb;
    [Header("Jump")]

    [SerializeField]
    private float jumpForce = 12f;
    [SerializeField]
    private float jumpCutMultiplier = 0.1f;

    private int jumpsleft = 2;

    [Header("Dash")]

    [SerializeField]
    private float dashSpeed = 20f;
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private float dashCooldown = 1f;
    [SerializeField]
    private float StamTimeRechargeAfter = 2f;

    [Header("Stamina")]

    [SerializeField]
    private float StamTimeEachRecharge = 0.7f;
    [SerializeField]
    private float Stamina = 5f;

    private float maxStamina;

    [Header("UI Settings")]
    [SerializeField]
    private Slider staminaSlider;

    public bool isDashing = false;
    private bool canDash = true;
    public bool canTakeDashDMG = true;

    private Coroutine stopRecharging;

    private GameObject child_playerTakeDMG;

    private int normalLayer;
    private int dashLayer;

    // Audio 
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip landingSound;

    void Awake()
    {
        child_playerTakeDMG = transform.Find("Player_Body").gameObject;
        rb = GetComponent<Rigidbody2D>();
        staminaSlider.maxValue = Stamina;
        staminaSlider.value = Stamina;
        maxStamina = Stamina;
        normalLayer = LayerMask.NameToLayer("Player");
        dashLayer = LayerMask.NameToLayer("PlayerDashing");
    }

    void Start()
    {
        staminaSlider.maxValue = Stamina;
        staminaSlider.value = Stamina;
        maxStamina = Stamina;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpsleft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsleft--;
            audioSource.PlayOneShot(jumpSound);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * jumpCutMultiplier
            );
        }

        HandleJumpAnimation();

        IFDash();
    }

    private void HandleJumpAnimation()
    {
        if (rb.linearVelocity.y > 0.1f)
        {
            animation.SetFloat("yVelocity", 1);
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            animation.SetFloat("yVelocity", -1);
        }
        else
        {
            animation.SetFloat("yVelocity", 0);
        }
    }

    private IEnumerator RechargeStamina()
    {
        
        yield return new WaitForSeconds(StamTimeRechargeAfter);

        while (Stamina < maxStamina)
        {
            Stamina += 1f;
            if (Stamina > maxStamina) Stamina = maxStamina;
            Debug.Log("Stamina: " + Stamina);
            staminaSlider.value = Stamina;

            yield return new WaitForSeconds(StamTimeEachRecharge);
        }

        stopRecharging = null;
    }


    private void IFDash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canDash && Stamina >= 1f)
        {
            if (stopRecharging != null) StopCoroutine(stopRecharging);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        if (!child_playerTakeDMG.GetComponent<PlayerTakeDMG>().isTakingDMG)
        {
            Debug.Log("Dash used.");
            animation.SetBool("isDashing", true);
            gameObject.layer = dashLayer;
            child_playerTakeDMG.layer = dashLayer;
            canDash = false;
            isDashing = true;
            Stamina -= 1f;
            staminaSlider.value = Stamina;

            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0;

            Vector2 direction = new Vector2(transform.localScale.x, 0);
            float startTime = Time.time;

            while ((Time.time < startTime + dashDuration) && !child_playerTakeDMG.GetComponent<PlayerTakeDMG>().isTakingDMG)
            {
                rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            child_playerTakeDMG.GetComponent<PlayerTakeDMG>().isTakingDMG = false;


            rb.gravityScale = originalGravity;
            isDashing = false;
            animation.SetBool("isDashing", false);

            if (stopRecharging != null) StopCoroutine(stopRecharging);
            stopRecharging = StartCoroutine(RechargeStamina());

            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            gameObject.layer = normalLayer;
            child_playerTakeDMG.layer = normalLayer;
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsleft = 2;
            audioSource.PlayOneShot(landingSound);
            animation.SetBool("isTouchingGround", true);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsleft = 2;
            animation.SetBool("isTouchingGround", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsleft = 1;
            animation.SetBool("isTouchingGround", false);
        }
    }
}