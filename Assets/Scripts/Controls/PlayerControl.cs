using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl: EntityControl
{
    private GameObject backShiledObject;
    private BackShield backShield;

    protected override void Start()
    {
        base.Start();
        SetBackShield();
    }
    protected override void MovementUpdate()
    {
        base.MovementUpdate();
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            HeavyAttack();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UltimateAttack();
        }
    }

    protected override void OnOrientationChanged()
    {
        base.OnOrientationChanged();
        if (isLookingRight)
        {
            backShiledObject.transform.localPosition = new Vector3(-Math.Abs(backShiledObject.transform.localPosition.x), backShiledObject.transform.localPosition.y, backShiledObject.transform.localPosition.z);
            SpriteRenderer attackSprite = backShiledObject.GetComponent<SpriteRenderer>();
            attackSprite.flipX = false;
            backShield.ExecuteAbility();
        }
        else
        {
            backShiledObject.transform.localPosition = new Vector3(Math.Abs(backShiledObject.transform.localPosition.x), backShiledObject.transform.localPosition.y, backShiledObject.transform.localPosition.z);
            SpriteRenderer attackSprite = backShiledObject.GetComponent<SpriteRenderer>();
            attackSprite.flipX = true;
            backShield.ExecuteAbility();
        }
    }

    private void SetBackShield()
    {
        backShiledObject = gameObject.transform.GetChild(5).gameObject.transform.GetChild(3).gameObject;
        backShield = backShiledObject.GetComponent<BackShield>();
    }

    public void AquiredPowerUp(float newSpeed, float newJumpForce, GameObject newAttacksPrefab, Sprite playerPowerUpSprite)
    {
        speed = newSpeed;
        jumpForce = newJumpForce;
        characterSprite.sprite = playerPowerUpSprite;
        StartCoroutine(ReplaceAttacks(newAttacksPrefab));
    }

    IEnumerator ReplaceAttacks(GameObject newAttacksPrefab)
    {
        yield return new WaitForEndOfFrame();
        GameObject newAttacksObject = Instantiate(newAttacksPrefab, abilityParent.transform.position, abilityParent.transform.rotation) as GameObject;
        newAttacksObject.transform.parent = gameObject.transform;
        DestroyImmediate(abilityParent);
        SetAbilities();
    }

    protected override void SetAbilities()
    {
        base.SetAbilities();
        SetBackShield();
    }

}
