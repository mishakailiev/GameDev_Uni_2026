using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobHealthAir : MonoBehaviour
{
    [SerializeField]
    private GameObject patrolBox;
    [Header("Settings")]
    [SerializeField]
    private float maxHealth = 10f;
    [SerializeField]
    private float currentHealth;

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
            Die();
    }

    private void Die()
    {
        Debug.Log("Mob has died.");
        audioSource.PlayOneShot(deathSound);
        WaitToDestroy();
        Destroy(patrolBox);
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
