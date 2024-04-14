﻿using UnityEngine;
using G04Telemetry;
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
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.EnemyReceiveDamageEvent(this.transform.position.x, this.transform.position.y));
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
