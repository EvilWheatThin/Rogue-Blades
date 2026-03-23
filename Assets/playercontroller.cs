using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;
    private bool isMoving = false;
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    damage dmg;
    
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {

                if (IsMoving && !touchingDirections.IsOnWall)

                    return walkSpeed;


                else

                    return 0f;
            }
            else
                return 0;
        }
    }
    public bool _isFacingRight = true;
    public float jumpImpulse = 10f;

    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    public bool IsAlive
    {

        get
        {
            return animator.GetBool("isAlive");
        }

    }



	private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        dmg = GetComponent<damage>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(!dmg.LockVelocity)
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        animator.SetFloat("yVelocity",rb.linearVelocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
            isMoving = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpImpulse);
        }

    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnHit(float dmg, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y+knockback.y);
		
    }

}
