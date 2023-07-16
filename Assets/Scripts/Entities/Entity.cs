using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float damageReduction = 0;
    public bool attackPreOrExecuting;
    protected Rigidbody2D entityRb;
    protected EntityControl entityControl;
    public bool isAlive;
    public Animator animator;
    protected Collider2D entityCollider;
    protected bool isShielded;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;
        attackPreOrExecuting = false;
        isShielded = false;
        entityRb = gameObject.GetComponent<Rigidbody2D>();
        entityControl = gameObject.GetComponent<EntityControl>();
        animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        entityCollider = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void DamageEntity(int damage, float knockBack, bool isAttackFromLeft)
    {
        if (isAlive)
        {
            entityControl.KnockBack(knockBack, isAttackFromLeft);
            animator.SetTrigger("damage");
            currentHealth -= (int)((float)damage*(1F-damageReduction));
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            if (currentHealth == 0)
            {
                EntityDeath();
            }
        }
    }

    public void HealEntity(int heal)
    {
        if (isAlive)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    protected virtual void EntityDeath()
    {
        isAlive = false;
        entityControl.CancelAbilities();
        entityControl.OnDeath();
        entityRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetTrigger("death");
    }
    
}
