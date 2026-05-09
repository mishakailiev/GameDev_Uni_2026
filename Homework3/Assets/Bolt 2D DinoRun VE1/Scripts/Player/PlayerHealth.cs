using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public int startingHealth = 3;
    public HealthUI healthUI;
    public LowHealthUI LowHealthUI;
    public CameraShake cameraShake;
    public SpriteRenderer sr;

    void Start()
    {
        healthUI.UpdateHearts(startingHealth);
        Initialize();
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (sr != null)
            StartCoroutine(Flash());

        health -= damage;

        if (healthUI != null)
            healthUI.UpdateHearts(health);

        if (health <= 0)
            Die();

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(0.2f, 0.3f));

        if (health <= 1)
        {
            LowHealthUI.ShowLowHealthEffect();
        }
        else
        {
            if (LowHealthUI != null)
            {
                LowHealthUI.HideEffect();
            }
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }
    }

    public void Initialize()
    {
        health = startingHealth;
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public void resetHealth()
    {
        health = startingHealth;
        healthUI.UpdateHearts(health);
        LowHealthUI.HideEffect();
    }

    IEnumerator Flash()
    {

        sr.color = new Color(1, 0, 0, 0.25f);

        yield return new WaitForSeconds(0.4f);

        sr.color = new Color(1, 0, 0, 0.0f);
    }

    void Die()
    {
        Debug.Log("Player Died");
        gameObject.SetActive(false);
        SceneManager.LoadScene("Demo");
    }


}