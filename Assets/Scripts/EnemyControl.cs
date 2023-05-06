using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyControl : EntityControl
{
    private GameObject player;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player = GameObject.Find("Player").gameObject;
    }

    new void Update()
    {
        base.Update();
        float towardsPlayerDirection = (player.transform.position.x - transform.position.x);
        if (Math.Abs(towardsPlayerDirection) > 1)
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
            RandomAttack();
        }
        
    }

    private void RandomAttack()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
        {
            heavyAttack.ExecuteAttack();
        }
        else
        {
            lightAttack.ExecuteAttack();
        }
    }

}
