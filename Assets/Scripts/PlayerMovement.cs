using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool canDoubleJump = true;
    private int currentDmg = 1;
    private float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float kbTimer;
    private Vector2 kbDirection;
    private Rigidbody2D enemyrb;
    

    


    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Animator animator;
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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Flip();

        ResetPlayerMovement();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        animator.SetInteger("isRunning", (int)horizontal);

    }

    public void Jump(InputAction.CallbackContext context)
    {

        if(context.performed)
        {
            if(IsGrounded() || canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                canDoubleJump = !canDoubleJump;
                animator.SetBool("isJumping", true);
            }
        }

        if(context.canceled)
        {
            animator.SetBool("isJumping", false);
            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime)
        {
            if (context.performed && IsGrounded())
            {
                animator.SetTrigger("isAttacking");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if(context.canceled)
        {
            
        }
    }

    private void ResetPlayerMovement()
    {
        //once knockback timer is over, reset player movement. 
        if (kbTimer > 0f)
        {
            kbTimer -= Time.deltaTime;
            if (kbTimer <= 0f)
            {
                rb.velocity = Vector2.zero;
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
            print(kbDirection);
            rb.velocity = Vector2.zero;
            enemyrb = enemy.GetComponent<Rigidbody2D>();
            enemyrb.AddForce(kbDirection * kbForce, ForceMode2D.Impulse);
            kbTimer = kbDuration;
            enemy.GetComponent<Enemy>().TakeDmg(currentDmg);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
