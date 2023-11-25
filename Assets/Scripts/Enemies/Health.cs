using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        print(gameObject.name + " took " + damage + " damage");
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}