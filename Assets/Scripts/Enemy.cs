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


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDmg(int dmg)
    {
        currentHealth -= dmg;

        animator.SetTrigger("isEnemyHurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        animator.SetBool("isEnemyDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}