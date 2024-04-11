using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//SIMONA ANTONOVA MIHAYLOVA

public class Cube : MonoBehaviour
{
    // tile al que corresponde la sala
    public GameObject tile;
    void OnTriggerEnter2D(Collider2D col)
    {
        // si el player está en la casilla
        if(col.gameObject.GetComponent<PlayerController>() != null)
        {
            // no se puede mover la casilla (deshabilitar colliders)
            GetComponent<Collider2D>().enabled = false;
            tile.GetComponent<Collider2D>().enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        // si el player no está en la casilla
        if(col.gameObject.GetComponent<PlayerController>() != null)
        {
            // habilitar colliders
            GetComponent<Collider2D>().enabled = true;
            tile.GetComponent<Collider2D>().enabled = true;
        }
    }
}
