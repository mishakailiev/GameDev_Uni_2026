using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private CameraShake cameraShake_;
    [SerializeField]
    private Animator animation;
    [SerializeField]
    private Collider2D player_collider;
    [Header("Settings")]
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentHealth;

    private float healthBeforeDeath;

    [Header("Invincibility")]
    [SerializeField]
    private float invincibilityDuration = 0.7f;
    private bool isInvincible = false;

    [Header("UI Settings")]
    [SerializeField]
    private Slider healthSlider;

    private Rigidbody2D rb;

    // Audio 
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip healSound;
    [SerializeField]
    private AudioClip takeDamageSound;

    void Start()
    {
        currentHealth = maxHealth;
        healthBeforeDeath = currentHealth;
        rb = GetComponent<Rigidbody2D>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Heal(float amount)
    {
        audioSource.PlayOneShot(healSound);
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        Debug.Log("Player Health: " + currentHealth);
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }
    
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;
    }

    public void HealAndIncreaseMaxHealth(float amount)
    {
        IncreaseMaxHealth(amount);
        Heal(amount);
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        audioSource.PlayOneShot(takeDamageSound);
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
        else
        {
            if (cameraShake_ != null)
                cameraShake_.Shake();
            StartCoroutine(BecomeInvincible());
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        animation.SetTrigger("Hurt");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        yield return new WaitForSeconds(invincibilityDuration);

        sr.color = originalColor;
        isInvincible = false;
        GameObject child_playerTakeDMG = transform.Find("Player_Body").gameObject;
        child_playerTakeDMG.GetComponent<PlayerTakeDMG>().isTakingDMG = false;
    }

    public void Die()
    {
        Debug.Log("Player Died");
        audioSource.PlayOneShot(deathSound);
        animation.SetTrigger("Die");
        player_collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Die();
        }
    }

    public void LoadSceneAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}