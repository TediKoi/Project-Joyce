using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool canDoubleJump = true;
    private bool wasJumping;
    
    

    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    public Animator animator;
    
    


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Flip();

        

    }

    private void FixedUpdate()
    {
        //this moves the player, horizontal is calculated from Move()
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //plays footsteps audio when moving
        if ((horizontal == 1f || horizontal == -1f) && IsGrounded())
        {
            AudioManager.GetInstance().FootstepsOn();
        }
        else
        {
            AudioManager.GetInstance().FootstepsOff();
        }
    }

    public bool IsGrounded()
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
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        
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
                AudioManager.GetInstance().PlaySFX(1);
                
            }
        }

        if(context.canceled)
        {
            animator.SetBool("isJumping", false);
            if(rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                wasJumping = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 6)
        {
            if(wasJumping == true)
            {
                AudioManager.GetInstance().PlaySFX(2);
                wasJumping = false;
            }
        }
    }

    

    

    
}
