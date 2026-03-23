using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
	public float attackDamage=25f;
    public Vector2 knockback=Vector2.zero;

	private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    
	private void OnTriggerEnter2D(Collider2D collision)
	{
		damage damage = collision.GetComponent<damage>();

        if (damage != null)
        {
            damage.Hit(attackDamage,knockback);
        }
	}
}
