using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityControl : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 30;
    protected bool isOnGround = false;
    protected SpriteRenderer characterSprite;
    protected GameObject abilityParent;
    protected List<GameObject> attackObjects = new List<GameObject>();
    public bool isLookingRight = true;
    protected bool jump = false;
    protected bool moveLeft = false;
    protected bool moveRight = false;
    public List<Ability> abilities = new List<Ability>();
    protected Rigidbody2D entitytRb;
    protected Entity entity;
    protected Vector3 knockBackVector = Vector3.zero;
    protected bool knockBack = false;
    public bool disableMovement = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterSprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        SetAbilities();
        entity = gameObject.GetComponent<Entity>();
        entitytRb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void SetAbilities()
    {
        abilityParent = gameObject.transform.GetChild(gameObject.transform.childCount-1).gameObject;
        attackObjects.Clear();
        abilities.Clear();
        foreach (Transform child in abilityParent.transform)
        {
            if (child.gameObject.GetComponent<BackShield>() == null)
            {
                attackObjects.Add(child.gameObject);
                abilities.Add(child.gameObject.GetComponent<Ability>());
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!disableMovement)
        {
            if (!moveLeft && !moveRight)
            {
                entity.animator.SetBool("isMoving", false);
            }
            if (moveLeft)
            {
                entity.animator.SetBool("isMoving", true);
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                moveLeft = false;
            }
            if (moveRight)
            {
                entity.animator.SetBool("isMoving", true);
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                moveRight = false;
            }
            if (jump)
            {
                entity.animator.SetBool("isMoving", false);
                entitytRb.velocity = Vector3.zero;
                entitytRb.AddForce(Vector3.up * jumpForce);
                jump = false;
            }
            if (knockBack)
            {
                entity.animator.SetBool("isMoving", false);
                entitytRb.velocity = Vector3.zero;
                entitytRb.AddForce(knockBackVector, ForceMode2D.Impulse);
                knockBack = false;
            }
        }
        else
        {
            entity.animator.SetBool("isMoving", false);
            moveLeft = false;
            moveRight = false;
            jump = false;
            knockBack = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (entity.isAlive)
        {
            MovementUpdate();
        }
    }

    protected virtual void MovementUpdate()
    {

    }

    protected void MoveLeft()
    {
        moveLeft = true;
        if (isLookingRight)
        {
            isLookingRight = false;
            OnOrientationChanged();
        }
    }

    protected void MoveRight()
    {
        moveRight = true;
        if (!isLookingRight)
        {
            isLookingRight = true;
            OnOrientationChanged();
        }
    }

    protected void Jump()
    {
        if (isOnGround)
        {
            jump = true;
            isOnGround = false;
            entity.animator.SetBool("isJumping", true);
        }
    }

    protected void LightAttack()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //   {
        abilities[0].ExecuteAbility(entity.animator,"lightAttack");
        //  }
    }

    protected void HeavyAttack()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //    {
        abilities[1].ExecuteAbility(entity.animator, "heavyAttack");
        //   }
    }

    protected void UltimateAttack()
    {
        if (abilities.Count == 3)
        {
            abilities[2].ExecuteAbility(entity.animator, "ultAttack");
        }
    }

    protected void RandomAttack(List<int> attacksThatHitIndexes)
    {
        int random = UnityEngine.Random.Range(0, attacksThatHitIndexes.Count - 1);
        switch (attacksThatHitIndexes[random])
        {
            case 0:
                LightAttack();
                break;
            case 1:
                HeavyAttack();
                break;
            case 2:
                UltimateAttack();
                break;
        }
    }

    public virtual void CancelAbilities()
    {
        foreach (Ability ability in abilities)
        {
            ability.CancelAbility();
        }
    }

    protected virtual void OnOrientationChanged()
    {
        if (isLookingRight)
        {
            foreach (GameObject attackObject in attackObjects)
            {
                attackObject.transform.localPosition = new Vector3(Math.Abs(attackObject.transform.localPosition.x), attackObject.transform.localPosition.y, attackObject.transform.localPosition.z);
                SpriteRenderer attackSprite = attackObject.GetComponent<SpriteRenderer>();
                attackSprite.flipX = false;
            }
            characterSprite.flipX = false;
        }
        else
        {
            foreach (GameObject attackObject in attackObjects)
            {
                attackObject.transform.localPosition = new Vector3(-Math.Abs(attackObject.transform.localPosition.x), attackObject.transform.localPosition.y, attackObject.transform.localPosition.z);
                SpriteRenderer attackSprite = attackObject.GetComponent<SpriteRenderer>();
                attackSprite.flipX = true;
            }
            characterSprite.flipX = true;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            entity.animator.SetBool("isJumping", false);
            isOnGround = true;
        }
        if(collision.gameObject.layer == 3)
        {
            Entity collisionEntity = collision.gameObject.GetComponent<Entity>();
            if (!collisionEntity.isAlive)
            {
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            }
        }
    }

    public void KnockBack(float knockBackAmount, bool isAttackFromLeft)
    {
        knockBackVector = new Vector3(2, 1, 0);
        if (isAttackFromLeft)
        {
            knockBackVector.x = -knockBackVector.x;
        }
        knockBackVector *= knockBackAmount;
        knockBack = true;
    }

    public virtual void OnDeath()
    {

    }
}
