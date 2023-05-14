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
    public bool isLookingLeft = true;
    protected bool jump = false;
    protected bool moveLeft = false;
    protected bool moveRight = false;
    public List<Attack> attacks = new List<Attack>();
    protected Rigidbody2D entitytRb;
    protected Entity entity;
    protected Vector3 knockBackVector = Vector3.zero;
    protected bool knockBack = false;
    protected bool disableMovement = false;

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
        abilityParent = gameObject.transform.GetChild(5).gameObject;
        attackObjects.Clear();
        attacks.Clear();
        foreach (Transform child in abilityParent.transform)
        {
            if (child.gameObject.GetComponent<BackShield>() == null)
            {
                attackObjects.Add(child.gameObject);
                attacks.Add(child.gameObject.GetComponent<Attack>());
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (moveLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            moveLeft = false;
        }
        if (moveRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            moveRight = false;
        }
        if (jump)
        {
            entitytRb.velocity = Vector3.zero;
            entitytRb.AddForce(Vector3.up * jumpForce);
            jump = false;
        }
        if (knockBack)
        {
            entitytRb.velocity = Vector3.zero;
            entitytRb.AddForce(knockBackVector, ForceMode2D.Impulse);
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
        if (!isLookingLeft)
        {
            isLookingLeft = true;
            OnOrientationChanged();
        }
    }

    protected void MoveRight()
    {
        moveRight = true;
        if (isLookingLeft)
        {
            isLookingLeft = false;
            OnOrientationChanged();
        }
    }

    protected void Jump()
    {
        if (isOnGround)
        {
            jump = true;
            isOnGround = false;
        }
    }

    protected void LightAttack()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //   {
        attacks[0].ExecuteAbility();
        //  }
    }

    protected void HeavyAttack()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //    {
        attacks[1].ExecuteAbility();
        //   }
    }

    protected void UltimateAttack()
    {
        if (attacks.Count == 3)
        {
            attacks[2].ExecuteAbility();
        }
    }

    protected void RandomAttack(List<int> attacksThatHitIndexes)
    {
        int random = UnityEngine.Random.Range(0, attacksThatHitIndexes.Count - 1);
        attacks[attacksThatHitIndexes[random]].ExecuteAbility();
    }

    protected virtual void OnOrientationChanged()
    {
        if (isLookingLeft)
        {
            foreach (GameObject attackObject in attackObjects)
            {
                attackObject.transform.localPosition = new Vector3(-Math.Abs(attackObject.transform.localPosition.x), attackObject.transform.localPosition.y, attackObject.transform.localPosition.z);
            }
            characterSprite.flipX = false;
        }
        else
        {
            foreach (GameObject attackObject in attackObjects)
            {
                attackObject.transform.localPosition = new Vector3(Math.Abs(attackObject.transform.localPosition.x), attackObject.transform.localPosition.y, attackObject.transform.localPosition.z);
            }
            characterSprite.flipX = true;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
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
}
