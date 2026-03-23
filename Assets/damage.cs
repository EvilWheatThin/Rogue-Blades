using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class damage : MonoBehaviour
{
    public UnityEvent<float, Vector2> dmgHit;
    public float maxHealth = 100;
    Animator animator;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    
    public float health = 100;

    public float Health { get { return health; } set {

            health = value;
            if (health <=0)
                IsAlive = false;

        }
    }

    public bool isAlive=true;
	private bool isInvincible=false;
    private float timeSinceHit = 0;
	public float invincibilityTime=.25f;

	public bool IsAlive { get { return isAlive; } private set { 
            isAlive = value;
            animator.SetBool("isAlive", value);
        } 
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }

    public void Awake()
	{
		animator = GetComponent<Animator>();
	}
	// Start is called before the first frame update
	public void Hit(float dmg, Vector2 knockback)
	{
        if (IsAlive && !isInvincible)
        {
            Health -= dmg;
            isInvincible = true;
            animator.SetTrigger("hit");
            LockVelocity = true;
            dmgHit?.Invoke(dmg, knockback);
            
        }
	}

	private void Update()
	{
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
	}
}
