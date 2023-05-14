using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyControl : EntityControl
{
    private GameObject playerObject;
    private Entity player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerObject = GameObject.Find("Player").gameObject;
        player = playerObject.GetComponent<Entity>();
    }

    protected override void MovementUpdate()
    {
        base.MovementUpdate();
        if (player.isAlive&& entity.isAlive)
        {
            List<int> attacksThatHitIndexes = new List<int>();
            int i = 0;
            foreach (Attack attack in attacks)
            {
                if (attack.objectsInRangeOfAttack.Count > 0)
                {
                    attacksThatHitIndexes.Add(i);
                }
                i++;
            }
            if (attacksThatHitIndexes.Count == 0)
            {
                float towardsPlayerDirection = (playerObject.transform.position.x - transform.position.x);
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
                RandomAttack(attacksThatHitIndexes);
            }
        }
        
    }

}
