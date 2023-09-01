using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform player;
    

    private enum State { Patrol, Attack };
    private State state;

    

    [Header("Patrol")]
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int patrolDestination;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        if(currentHealth > 0)
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
                if (Vector2.Distance(transform.position, player.position) < 2f)
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
                if (Vector2.Distance(transform.position, player.transform.position) < 2f)
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

    public void TakeDmg(int dmg)
    {
        
        currentHealth -= dmg;
        animator.SetTrigger("isHurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        animator.SetTrigger("isDead");
        
    }

    
}
