using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public Animator animator;

    private float attackRate = 1;
    private float nextAttackTime = 0f;

    void Update()
    {
        // si ha pasado más tiempo del cooldown
        if(Time.time >= nextAttackTime)
        {
            // ataque
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("v"))
            {
                Attacking();
                nextAttackTime = (Time.time + 1f)/ attackRate;
                // animacion del ataque
                animator.SetBool("IsAttacking", true);
            }
            else
            {
                // desactivar animacioin
                animator.SetBool("IsAttacking", false);
            }
        }
    }
    
    void Attacking()
    {
        // detectar si el enemigo está en el rango de ataque a melee
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // hacer daño a todos los enemigos que estén en el rango
        foreach(Collider2D enemy in hitEnemies)
        {
            // acceder al componente de cada enemigo
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            Debug.Log("Damage:"+enemy.name);
        }
    }

    // dibuja el radio de ataque en el inspector solo cuando está seleccionada
    void OnDrawGizmosSelected()
    {
        // si no hay punto de ataque se sale del metodo
        if(attackPoint == null)
        {
            return;
        }
        // se dibuja una esfera del tamaño del rango
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
