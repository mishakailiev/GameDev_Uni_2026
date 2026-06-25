using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobHealth : MonoBehaviour
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
        currentHealth -= damage;
        Debug.Log("Mob Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animation.SetTrigger("TakeDMG");
        }
    }

    private void Die()
    {
        Debug.Log("Mob has died.");
        isDead = true;
        if(animation  != null) { 
            audioSource.PlayOneShot(deathSound);
            animation.SetTrigger("Die");
        }
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void DestroyTheMod()
    {
        Destroy(gameObject);
        Debug.Log("Mob Destroyed!");
    }
}
