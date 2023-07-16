using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AerialEnemyControl : EntityControl
{
    private GameObject playerObject;
    private Entity player;
    public float distanceFromPlayer;
    public float detectionDistance;

    protected override void Start()
    {
        base.Start();
        playerObject = GameObject.Find("Player").gameObject;
        player = playerObject.GetComponent<Entity>();
    }

    protected override void MovementUpdate()
    {
        base.MovementUpdate();
        if (player.isAlive && entity.isAlive)
        {
            float towardsPlayerDirection = (playerObject.transform.position.x - transform.position.x);
            if (MathF.Abs(towardsPlayerDirection) < detectionDistance)
            {
                if (Math.Abs(towardsPlayerDirection) > distanceFromPlayer)
                {
                    if (towardsPlayerDirection > 0)
                    {
                        MoveRight();
                    }
                    else if (towardsPlayerDirection < 0)
                    {
                        MoveLeft();
                    }
                }
                else
                {
                    LightAttack();
                }
            }
            RaycastHit2D hit= Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, LayerMask.GetMask("Environment"));
            if (hit.collider!=null)
            {
                // Calculate the distance from the surface and the "error" relative
                // to the floating height.
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                float heightError = 5 - distance;
                if (hit.distance > 5 || hit.distance < 5)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 5 - hit.distance, transform.position.z);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            }
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 6;
    }
}
