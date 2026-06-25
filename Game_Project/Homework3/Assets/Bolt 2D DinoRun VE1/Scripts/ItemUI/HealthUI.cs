using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;

    public int health;

    public void UpdateHearts(int currentHealth)
    {
        health = currentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < health);

        }
        Debug.Log("Updating hearts: " + health);
    }
}