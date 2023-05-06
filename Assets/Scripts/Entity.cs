using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool attackExecuting;
    private Rigidbody2D entityRb;
    private EntityControl entityControl;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        attackExecuting = false;
        entityRb = gameObject.GetComponent<Rigidbody2D>();
        entityControl = gameObject.GetComponent<EntityControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEntity(int damage, float knockBack, bool isAttackFromLeft)
    {
        Animator animator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        entityControl.KnockBack(knockBack, isAttackFromLeft);
        animator.SetTrigger("damage");
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (currentHealth == 0)
        {
            EntityDeath();
        }
    }

    public void HealEntity(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void EntityDeath()
    {
        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
