using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobHealthGround : MonoBehaviour
{
    [SerializeField]
    private Animator animation;
    [Header("Settings")]
    [SerializeField]
    private float maxHealth = 10f;
    [SerializeField]
    private float currentHealth;

    private bool isDead = false;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip deathSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        animation.SetTrigger("TakeDMG");
        currentHealth -= damage;
        Debug.Log("Mob Health: " + currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Mob has died.");
        isDead = true;
        audioSource.PlayOneShot(deathSound);
        animation.SetTrigger("Die");
        StartCoroutine(WaitToDestroy());
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
