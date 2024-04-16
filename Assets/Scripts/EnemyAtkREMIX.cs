using System.Collections;
using UnityEngine;

//Código implementado por:
//ENRIQUE JUAN GAMBOA

public class EnemyAtkREMIX : MonoBehaviour
{
    //Dist. de detección, Dist. a la que se para
    public float rangeOfDetection = 2f, rangeOfAttack = 1f, attackRate = 1f, attackDuration = 1f;
    public int damage;
    //Velocidad
    public int speed = 1;
    //Objeto al que debe perseguir
    public GameObject imAngryWith;
    public LayerMask toFollow; 

    private Collider2D daCollider;
    private bool isItAtkTime = true;
    private string collided;

    private void Start()
    {
        daCollider = GetComponent<CapsuleCollider2D>();
        //Desactivo collider al hacer el raycast para que no se detecte a sí mismo
        daCollider.enabled = false;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, imAngryWith.transform.position - transform.position, rangeOfDetection, toFollow);
        //Para saber qué está "viendo"

        //Problemas con los objetos "raycasteados" no teniendo nombres
        try
        {
            collided = hit.collider.name;
        }
        catch
        {
            collided = null;
        }

        //Convierto en vector 2 la pos. de lo que tiene que perseguir
        Vector2 imAngryPos = new Vector2(imAngryWith.transform.position[0], imAngryWith.transform.position[1]);
        //Distancia a la que está de su objetivo
        float distSense = Vector2.Distance(transform.position, imAngryPos);

        //Se activa si es el objetivo
        if (collided == imAngryWith.name && rangeOfDetection > distSense)
        {
            //Mira si lo tiene que perseguir o atacar
            if (rangeOfAttack < distSense)
            {
                //Movimiento!
                transform.position = Vector2.MoveTowards(transform.position, imAngryPos, speed * Time.deltaTime);
                
                Debug.DrawRay(transform.position, imAngryWith.transform.position - transform.position, Color.yellow);
            }
            //Ataca!
            else
            {
                Debug.DrawRay(transform.position, imAngryWith.transform.position - transform.position, Color.red);
                daCollider.enabled = true;
                Attack();
            }
        }
        //Si no es el objetivo...
        else
        {
            Debug.DrawRay(transform.position, imAngryWith.transform.position - transform.position, Color.gray);
            daCollider.enabled = false;
        }
    }

    //Pendiente de implementar
    private void Attack()
    {
        if (isItAtkTime)
        {
            StartCoroutine("AttackDurator");
            isItAtkTime = false;
            StartCoroutine("AttackWaiter");
        }
    }
    public IEnumerator AttackDurator()
    {
        daCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration * 0.1f);
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerReceiveDamageEvent(G04Telemetry.EnemyType.Robot));
        imAngryWith.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    public IEnumerator AttackWaiter()
    {
        daCollider.enabled = false;
        yield return new WaitForSeconds(attackRate);
        isItAtkTime = true;
    }
}
