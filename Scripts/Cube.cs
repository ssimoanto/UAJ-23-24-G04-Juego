using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        // si el player está en la casilla
        if(col.gameObject.GetComponent<PlayerController>() != null)
        {
            col.transform.parent = this.transform;
        }
    }
}
