using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    private enum State { Patrol, Attack };
    private State state;
    

    [Header("Default")]
    [SerializeField]
    public int currentHealth;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private ParticleSystem blood;
    private BoxCollider2D collider;


    [Header("Attacking")]
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float kbForce;
    [SerializeField]
    private float kbDuration;
    private float kbTimer;
    [SerializeField]
    private Vector2 kbDirection;
    [SerializeField]
    private Vector2 force;
    [SerializeField]
    private float upwardForce;
    [SerializeField]
    private Rigidbody2D playerRb;
    [SerializeField]
    private int currentDmg;
    [SerializeField]
    private Collider2D[] playerHit;


    [Header("Patrol")]
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int patrolDestination;
    [SerializeField]
    private bool tookDmg;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        state = State.Patrol;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                EnemyAttack();
                break;
            
            
        }

    }

    private void Patrol()
    {
        
        if(currentHealth > 0 && !tookDmg)
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                //check if he reached destination, then goes to other destination
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(-3.6f, 3.6f, 3.6f);
                    patrolDestination = 1;
                }
                //check if player is close, then attacks
                if (Vector2.Distance(transform.position, player.position) < 1f)
                {
                    animator.SetBool("isWalking", false);
                    state = State.Attack;
                    
                }
                
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                //check if he reached destination, then goes to other destination
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
                    patrolDestination = 0;
                }
                //check if player is close, then attacks
                if (Vector2.Distance(transform.position, player.transform.position) < 1f)
                {
                    animator.SetBool("isWalking", false);
                    state = State.Attack;
                }
                
            }
        }

        
    }

    private void EnemyAttack()
    {
        animator.SetBool("isAttacking", true);
        //check if player is far from enemy, then keep patrolling
        if (Vector2.Distance(transform.position, player.transform.position) > 2f)
        {
            animator.SetBool("isAttacking", false);
            state = State.Patrol;
        }
        
    }

    
    public void EnemyMeleeAttackAnimation()
    {
        //check to see what enemies you hit and put it in a arraylist
        playerHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);


        foreach (Collider2D player in playerHit)
        {
            kbDirection = (player.transform.position - transform.position);
            force = kbDirection * kbForce + Vector2.up * upwardForce;
            playerRb = player.GetComponent<Rigidbody2D>();
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(force, ForceMode2D.Impulse);
            kbTimer = kbDuration;
            player.GetComponent<PlayerHealth>().TakeDmg(currentDmg);
        }
    }
   

    

    public void TakeDmg(int dmg)
    {
        tookDmg = true;
        currentHealth -= dmg;
        animator.SetTrigger("isHurt");
        AudioManager.GetInstance().GoblinSFX(5);
        blood.Play();
        if(currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(TookDmg());
    }

    IEnumerator TookDmg()
    {
        yield return new WaitForSeconds(0.3f);
        tookDmg = false;
    }

    void Die()
    {
        Debug.Log("enemy died");
        animator.SetTrigger("isDead");
        AudioManager.GetInstance().GoblinSFX(6);
        collider.enabled = false;
        rb.simulated = false;
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    


}
