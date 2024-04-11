using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//ALEJANDRO MARTÍNEZ

public class Sierra2 : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speed;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rbody.velocity = new Vector2(speed, rbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pared")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x , this.transform.localScale.y);
        }
    }
}
