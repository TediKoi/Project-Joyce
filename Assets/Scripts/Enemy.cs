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
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
            //check if he reached destination
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(-3.6f, 3.6f, 3.6f);
                patrolDestination = 1;
            }
        }
        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
            //check if he reached destination
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
                patrolDestination = 0;
            }
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
