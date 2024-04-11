using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//CARLOS TOMÁS, ALEJANDRO MARTINEZ, SIMONA ANTONOVA

public class PlayerController : MonoBehaviour
{
    public Vector3 initialPos;
    public GameObject gas;
    public bool disable;
    public GameObject player;
    public float velocity = 5f, immunityPostHit = 0.5f;
    public Animator animator;
    
    private float horizontal, vertical;
    private bool tiempoespera = false;
    private bool tiempogas = false;
    private SpriteRenderer spriteGas;
    private SpriteRenderer mySpriteRenderer;
    private PlayerHealth vida;
    private Rigidbody2D rb;

    void Awake()
    {
        spriteGas = gas.GetComponent<SpriteRenderer>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        vida = GetComponent<PlayerHealth>();
        spriteGas.enabled = false;
        initialPos = gameObject.transform.position;
    }

     void Start()
     {
        StartCoroutine(Freeze());
        StartCoroutine(Startgame());
     }
    void Update()
    {
        if (!disable)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
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
        print("ha entrado");
        tiempoespera = false;
    }
    
    private void OnTriggerStay2D(Collider2D gas)
    {
        if(gas.tag == "Gas" && !tiempogas)
        {
            vida.TakeDamage(10);
            spriteGas.enabled = true;
            tiempogas = true;
            StartCoroutine("EsperarGas");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Gas")
        { 
            spriteGas.enabled = false;
        }
    }

    public IEnumerator EsperarGas()
    {
        yield return new WaitForSeconds(2);
        tiempogas = false;    
    }
    public IEnumerator Freeze()
    {
        disable = true;      
        yield return new WaitForSeconds(15);
        
    }
    public IEnumerator Startgame()
    {
        yield return new WaitForSeconds(16);
        disable = false;
    }


}
