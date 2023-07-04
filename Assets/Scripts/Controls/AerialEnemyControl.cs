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
        }
    }
}
