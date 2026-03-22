using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public int startingHealth = 3;
    public HealthUI healthUI;
    public LowHealthUI LowHealthUI;

    void Start()
    {
        healthUI.UpdateHearts(startingHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0)
            health = 0;

        healthUI.UpdateHearts(health);

        if (health <= 0)
        {
            Die();
        }

        if (health <= 1)
        {
            LowHealthUI.ShowLowHealthEffect();
        }
        else
        {
            LowHealthUI.HideEffect();
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void resetHealth()
    {
        health = startingHealth;
        healthUI.UpdateHearts(health);
        LowHealthUI.HideEffect();
    }

    void Die()
    {
        Debug.Log("Player Died");
        gameObject.SetActive(false);
        SceneManager.LoadScene("Demo");
    }
}