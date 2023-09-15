using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHealth = 5;
    [SerializeField]
    private float currentHealth = 0;
    [SerializeField]
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(float dmg)
    {
        currentHealth -= dmg;
        playerMovement.animator.SetTrigger("isHurt");

        if (currentHealth <= 0)
        {
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        Debug.Log("Player Dead");
        playerMovement.animator.SetTrigger("isDead");
    }
}
