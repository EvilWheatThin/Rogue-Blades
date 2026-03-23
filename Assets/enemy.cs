using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
    
{
    Animator animator;
    public float walkSpeed = 3f;
    public detection attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    damage dmg;
    public bool hasTarget=false;
	public bool HasTarget { get { return hasTarget; }
        private set {
            hasTarget = value;
            animator.SetBool("hasTarget",value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

	private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        dmg = GetComponent<damage>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
        if (!dmg.LockVelocity)
            {
            if (CanMove)
                rb.linearVelocity = new Vector2(walkSpeed * (transform.localScale.x > 0 ? 1 : -1), rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void FlipDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
        
    }
}

