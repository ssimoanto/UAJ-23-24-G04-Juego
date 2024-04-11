using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//ALEJANDRO MARTÍNEZ

public class MoviSierra : MonoBehaviour
{
    public Transform target;
    private float speed = 1f;
    
    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime ));
    }
    public void OntriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag ==" Sierra")
        {
            target = other.gameObject.GetComponent<WP1>().NextPoint;
            transform.LookAt(new Vector2(target.position.x, target.position.y));
        }
    }
}
