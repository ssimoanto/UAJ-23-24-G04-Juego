using UnityEngine;
using G04Telemetry;

//Código implementado por:
//SIMONA ANTONOVA MIHAYLOVA Y EDUARDO GALLARDO

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public Animator animator;
    public GameObject prefabParticula;

    private float attackRate = 1f;
    private float nextAttackTime = 0f;
    ParticleSystem ps;

    private void Start()
    {
        // variable del sistema de particulas
        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;
    }
    void Update()
    {
        // si ha pasado más tiempo del cooldown
        if(Time.time >= nextAttackTime)
        {
            // se puede atacar
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("v"))
            {
                Attacking();
                GameManager.GetInstance().Sounds(0);
                nextAttackTime = (Time.time + 1f)/ attackRate;
            }
            
        }
    }
    
    void Attacking()
    {
        print(G04Telemetry.Tracker.Instance());
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.AttackEvent());
        // animacion del ataque
        animator.SetTrigger("IsAttacking");
        // detectar si el enemigo está en el rango de ataque a melee
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // hacer daño a todos los enemigos que estén en el rango
        foreach(Collider2D enemy in hitEnemies)
        {
            // acceder al componente de cada enemigo
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            // emitir partículas
            var emission = ps.emission;
            emission.enabled = true;
            ps.Emit(2);
        }
    }

    // dibuja el radio de ataque en el editor solo cuando está seleccionado el player
    void OnDrawGizmosSelected()
    {
        // se dibuja una esfera del tamaño del rango
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
