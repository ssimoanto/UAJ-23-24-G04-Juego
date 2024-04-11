using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sierra2 : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speed;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = new Vector2(speed, rbody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pared")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
    }
}
