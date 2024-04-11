using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnemy1 : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float speedy;
    public float speedx;
    public float speedaux = 1;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        speedy = 1;
        speedx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = new Vector2(speedx,speedy);
       
    }
   
}
