using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private float damage = 2f;

    public float IncreaseDamage(float additionalDamage)
    {
        damage += additionalDamage;
        return damage;
    }

    public float SubtractDamage(float damageToSubtract)
    {
        damage -= damageToSubtract;
        if (damage < 0f) damage = 0f;
        return damage; 
    }

    public float GetDamage()
    {
        return damage;
    }
}
