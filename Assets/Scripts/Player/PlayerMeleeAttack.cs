using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Melee Attack")]
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRange = 0.5f;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private float kbForce;
    [SerializeField]
    private float kbDuration;


    private int currentDmg = 1;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float kbTimer;
    private PlayerMovement playerMovement;
    private Vector2 kbDirection;
    private Rigidbody2D enemyrb;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetPlayerMovement();
    }

    private void ResetPlayerMovement()
    {
        //once knockback timer is over, reset player movement. 
        if (kbTimer > 0f)
        {
            kbTimer -= Time.deltaTime;
            if (kbTimer <= 0f)
            {
                playerMovement.rb.velocity = Vector2.zero;
            }
        }
    }

    public void AttackDmg()
    {
        //check to see what enemies you hit and put it in a arraylist
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach (Collider2D enemy in enemiesHit)
        {
            kbDirection = (enemy.transform.position - transform.position).normalized;
            kbDirection.y = 1f;
            enemyrb = enemy.GetComponent<Rigidbody2D>();
            enemyrb.velocity = Vector2.zero;
            enemyrb.AddForce(kbDirection * kbForce, ForceMode2D.Impulse);
            kbTimer = kbDuration;
            enemy.GetComponent<Enemy>().TakeDmg(currentDmg);

        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        if (Time.time >= nextAttackTime)
        {
            if (context.performed && playerMovement.IsGrounded())
            {
                playerMovement.animator.SetTrigger("isAttacking");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (context.canceled)
        {

        }
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
