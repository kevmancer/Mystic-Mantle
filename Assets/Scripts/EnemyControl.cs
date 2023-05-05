using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyControl : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 30;
    private bool isOnGround = false;
    private SpriteRenderer characterSprite;
    private GameObject punch, kick;
    private bool isLookingLeft = true;
    private bool jump = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private Attack punchAttack;
    private Attack kickAttack;

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        punch = gameObject.transform.GetChild(5).gameObject;
        kick = gameObject.transform.GetChild(6).gameObject;
        punchAttack = punch.gameObject.GetComponent<Attack>();
        kickAttack = kick.gameObject.GetComponent<Attack>();
    }

    // Update is called once per frame
    void FixedUpdate()
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

    private void Update()
    {
        //enemy movement

    }

    private void MoveLeft()
    {
        moveLeft = true;
        isLookingLeft = true;
        SetPlayerOrientation();
    }

    private void MoveRight()
    {
        moveRight = true;
        isLookingLeft = false;
        SetPlayerOrientation();
    }

    private void Jump()
    {
        if (isOnGround) 
        {
            jump = true;
            isOnGround = false;
        }
    }

    private void Punch()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //   {
        punchAttack.ExecuteAttack();
        //  }
    }

    private void Kick()
    {
        //    if (!punchAttack.attackExecuting && !kickAttack.attackExecuting)
        //    {
        kickAttack.ExecuteAttack();
        //   }
    }

    private void SetPlayerOrientation()
    {
        if (isLookingLeft)
        {
            punch.transform.localPosition = new Vector3(-Math.Abs(punch.transform.localPosition.x), punch.transform.localPosition.y, punch.transform.localPosition.z);
            kick.transform.localPosition = new Vector3(-Math.Abs(kick.transform.localPosition.x), kick.transform.localPosition.y, kick.transform.localPosition.z);
            characterSprite.flipX = false;
        }
        else
        {
            punch.transform.localPosition = new Vector3(Math.Abs(punch.transform.localPosition.x), punch.transform.localPosition.y, punch.transform.localPosition.z);
            kick.transform.localPosition = new Vector3(Math.Abs(kick.transform.localPosition.x), kick.transform.localPosition.y, kick.transform.localPosition.z);
            characterSprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
