using UnityEngine;

//Código implementado por:
//SIMONA ANTONOVA MIHAYLOVA Y EDUARDO GALLARDO

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // quitar vida 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // si la vida llega a 0, el enemigo muere
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.GetInstance().Sounds(2);
        // se destruye el enemigo
        Destroy(gameObject);
    }
}
