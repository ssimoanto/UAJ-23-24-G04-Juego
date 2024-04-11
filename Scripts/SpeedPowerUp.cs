using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float multiplier = 2f, duration = 5f;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerHealth>())
        {
            GameObject player = col.gameObject;
            StartCoroutine(Speed(player));
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    IEnumerator Speed(GameObject player)
    {
        player.GetComponent<PlayerController>().velocity *= multiplier;

        

        yield return new WaitForSeconds(duration);
        player.GetComponent<PlayerController>().velocity /= multiplier;

        Destroy(this.gameObject);
    }
}
