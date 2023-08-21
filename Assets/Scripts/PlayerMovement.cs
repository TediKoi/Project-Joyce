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



    // Update is called once per frame
    void Update()
    {
        
        Flip();

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

    private void AttackDmg()
    {
        print("did dmg");
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
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in enemiesHit)
        {
            print("We hit " + enemy.name);
        }

        if(context.performed && IsGrounded())
        {
            animator.SetTrigger("isAttacking");
        }
        if(context.canceled)
        {
            
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
