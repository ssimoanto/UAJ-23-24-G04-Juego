using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviSierra : MonoBehaviour
{

    float speed = 1f;
    public Transform target;
    
    // Update is called once per frame
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
