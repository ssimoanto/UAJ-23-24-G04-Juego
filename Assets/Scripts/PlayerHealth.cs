using UnityEngine;
using G04Telemetry;
//Código implementado por:
//SIMONA ANTONOVA MIHAYLOVA Y EDUARDO GALLARDO

public class PlayerHealth : MonoBehaviour
{
    public float health = 0f, maxHealth = 200f;
    public Animator animator;
    public LifeBar healthBar;

    private float cooldown = 0f;
    
    void Awake()
    {
        health = maxHealth;
    }

    // actualizar la barra de vida
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    // daño
    public void TakeDamage(int damage)
    {
        if(damage>0)
        {
            GameManager.GetInstance().Sounds(1);
        }
        health -= damage;
        healthBar.SetHealth(health);

        // animación de herido
        animator.SetTrigger("Hurt");
        if (Time.time >= cooldown)
        {
            cooldown = (Time.time + 1f);
        }
    }
    void Update()
    {
        if (health<=0)
        {
            G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerDeadEvent(this.transform.position.x, this.transform.position.y));
            GameManager.GetInstance().GameOver();
        }
    }

}
