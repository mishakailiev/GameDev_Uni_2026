using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item settings")]
    [SerializeField]
    private bool isHealItem;
    [SerializeField]
    private bool isHealthIncreaseItem;
    [SerializeField]
    private bool isIHealAndIncreaseHealthItem;

    [Header("Item visual settings")]
    [SerializeField]
    private float HealItem;
    [SerializeField]
    private float HealthIncreaseItem;
    [SerializeField]
    private float IHealAndIncreaseHealthItem;

    [Header("Game player settings")]
    [SerializeField]
    private PlayerHealth playerhealth;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerhealth == null)
            {
                Debug.Log("Player health is not set to an object!");
                return;
            }
            if (isHealItem)
            {
                playerhealth.Heal(HealItem);
            }
            else if (isHealthIncreaseItem)
            {
                playerhealth.IncreaseMaxHealth(HealthIncreaseItem);
            }
            else if (isIHealAndIncreaseHealthItem)
            {
                playerhealth.HealAndIncreaseMaxHealth(IHealAndIncreaseHealthItem);
            }
            Destroy(gameObject);
        }
    }
}
