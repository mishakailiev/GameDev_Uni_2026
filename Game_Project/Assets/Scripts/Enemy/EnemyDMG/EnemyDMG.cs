using UnityEngine;

public class EnemyDMG : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

    public void AddDamage(int amount)
    {
        damage += amount;
    }

    public void RemoveDamage(int amount)
    {
        damage -= amount;
        if(damage <= 0)
        {
            damage = 0;
        }
    }
}
