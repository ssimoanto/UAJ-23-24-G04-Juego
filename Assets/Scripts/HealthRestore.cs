using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//EDUARDO GALLARDO Y SIMONA ANTONOVA
public class HealthRestore : MonoBehaviour
{
    public float restore;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerHealth>())
        {
            GameObject player = col.gameObject;
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth.health < playerHealth.maxHealth)
            {
                if (playerHealth.health + restore <= playerHealth.maxHealth)
                {
                    playerHealth.health += restore;
                    GameManager.GetInstance().Sounds(4);

                    // llamo a este metodo para actualizar la barra de vida en la pantalla
                    // para no poner otra variable publica en este script
                    playerHealth.TakeDamage(0);
                }
                else
                {
                    playerHealth.health = playerHealth.maxHealth;
                    playerHealth.TakeDamage(0);
                }

                Destroy(this.gameObject);
            }
        }
    }
}
