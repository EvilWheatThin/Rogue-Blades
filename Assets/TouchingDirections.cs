using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider;
    public ContactFilter2D contactFilter;
    RaycastHit2D[] gHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    public float wallCheckDistance = .2f;
    public float ceilingDistance = .05f;
    public float groundDistance=.05f;
    private bool isGrounded;
    Animator animator;
    private bool isOnWall;
    private bool isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
	public bool IsGrounded { 
        get 
        {
            return isGrounded;
                } 
        private set 
        {
            isGrounded = value;
            animator.SetBool("isGrounded", value);
        } 
    }
    public bool IsOnWall
    {
        get
        {
            return isOnWall;
        }
        private set
        {
            isOnWall = value;
            animator.SetBool("isOnWall", value);
        }
    }

    public bool IsOnCeiling
    {
        get
        {
            return isOnCeiling;
        }
        private set
        {
            isOnWall = value;
            animator.SetBool("isOnCeiling", value);
        }
    }

    private void Awake()
	{
		capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       IsGrounded = capsuleCollider.Cast(Vector2.down, contactFilter, gHit, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallCheckDistance) > 0;
            IsOnCeiling= capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
