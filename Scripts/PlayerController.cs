using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    PlayerHealth vida;
    Rigidbody2D rb;
    public float velocity = 5f, immunityPostHit = 0.5f;
    public Animator animator;
    float horizontal, vertical;
    public bool tiempoespera = false;
    public bool tiempogas = false;

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        vida = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Control();
    }
    void Control()
    {
        rb.velocity = new Vector2(horizontal, vertical).normalized * velocity;
        if(horizontal < -0.1f)
        {
            if(mySpriteRenderer != null)
            {
                // flip the sprite
                mySpriteRenderer.flipX = true;
            }
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D oneHitEnemy)
    {
        if (!tiempoespera)
        {
            if (oneHitEnemy.tag == "Sierra")
            {
                vida.TakeDamage(30);
                tiempoespera = true;
                StartCoroutine("EsperarGolpe");
            }
            else if (oneHitEnemy.tag == "Enemy")
            {
                vida.TakeDamage(50);
                tiempoespera = true;
                StartCoroutine("EsperarGolpe");
            }
        }
    }
    public IEnumerator EsperarGolpe()
    {
        yield return new WaitForSeconds(immunityPostHit);
        tiempoespera = false;
    }
    
    private void OnTriggerStay2D(Collider2D gas)
    {
        if(gas.tag == "Gas" && !tiempogas)
        {                      
            vida.TakeDamage(10);
            tiempogas = true;
            StartCoroutine("EsperarGas");
        }
    }

    public IEnumerator EsperarGas()
    {
        yield return new WaitForSeconds(2);
        tiempogas = false;
    }


}
