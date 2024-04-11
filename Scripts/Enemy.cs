using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // quitar vida 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // si la vida llega a 0, muere
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        // se destruye el enemigo
        Destroy(gameObject);
    }
}
