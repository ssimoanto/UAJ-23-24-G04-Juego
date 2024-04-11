using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//EDUARDO GALLARDO

public class DamagePowerUp : MonoBehaviour
{
    public int multiplier, duration;
    void OnTriggerEnter2D(Collider2D col)
    {
        //si la colision es el player
        if (col.gameObject.GetComponent<PlayerController>() != null)
        {
            GameObject player = col.gameObject;
            // hacer daño cada cierto tiempo
            StartCoroutine(Damage(player));
                
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator Damage(GameObject player)
    {
        player.GetComponent<Attack>().attackDamage *= multiplier;

        yield return new WaitForSeconds(duration);
        player.GetComponent<Attack>().attackDamage /= multiplier;

        Destroy(this.gameObject);
    }
}
