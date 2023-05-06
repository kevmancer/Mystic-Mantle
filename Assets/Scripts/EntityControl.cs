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
    protected GameObject lightAttackObject, heavyAttackObject;
    public bool isLookingLeft = true;
    protected bool jump = false;
    protected bool moveLeft = false;
    protected bool moveRight = false;
    protected Attack lightAttack;
    protected Attack heavyAttack;
    protected Rigidbody2D entitytRb;

    // Start is called before the first frame update
    protected void Start()
    {
        characterSprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        lightAttackObject = gameObject.transform.GetChild(5).gameObject;
        heavyAttackObject = gameObject.transform.GetChild(6).gameObject;
        lightAttack = lightAttackObject.gameObject.GetComponent<Attack>();
        heavyAttack = heavyAttackObject.gameObject.GetComponent<Attack>();
        entitytRb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
            rb.AddForce(Vector3.up * jumpForce);
            jump = false;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void MoveLeft()
    {
        moveLeft = true;
        isLookingLeft = true;
        SetEntityOrientation();
    }

    protected void MoveRight()
    {
        moveRight = true;
        isLookingLeft = false;
        SetEntityOrientation();
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
        lightAttack.ExecuteAttack();
        //  }
    }

    protected void HeavyAttack()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //    {
        heavyAttack.ExecuteAttack();
        //   }
    }

    protected void SetEntityOrientation()
    {
        if (isLookingLeft)
        {
            lightAttackObject.transform.localPosition = new Vector3(-Math.Abs(lightAttackObject.transform.localPosition.x), lightAttackObject.transform.localPosition.y, lightAttackObject.transform.localPosition.z);
            heavyAttackObject.transform.localPosition = new Vector3(-Math.Abs(heavyAttackObject.transform.localPosition.x), heavyAttackObject.transform.localPosition.y, heavyAttackObject.transform.localPosition.z);
            characterSprite.flipX = false;
        }
        else
        {
            lightAttackObject.transform.localPosition = new Vector3(Math.Abs(lightAttackObject.transform.localPosition.x), lightAttackObject.transform.localPosition.y, lightAttackObject.transform.localPosition.z);
            heavyAttackObject.transform.localPosition = new Vector3(Math.Abs(heavyAttackObject.transform.localPosition.x), heavyAttackObject.transform.localPosition.y, heavyAttackObject.transform.localPosition.z);
            characterSprite.flipX = true;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    public void KnockBack(float knockBack, bool isAttackFromLeft)
    {
        Vector3 knockBackDirection = new Vector3(2, 1, 0);
        if (isAttackFromLeft)
        {
            knockBackDirection.x = -knockBackDirection.x;
        }
        entitytRb.AddForce(knockBackDirection.normalized * knockBack, ForceMode2D.Impulse);
    }
}
