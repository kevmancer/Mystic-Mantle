using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyControl : EntityControl
{
    private GameObject playerObject;
    private Entity player;
    public float detectionDistance;

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
            float towardsPlayerDirectionDistance = (playerObject.transform.position.x - transform.position.x);
            if (Mathf.Abs(towardsPlayerDirectionDistance) < detectionDistance)
            {
                int i = 0;
                foreach (Attack attack in abilities)
                {
                    if (attack.objectsInRangeOfAttack.Count > 0&&!attack.isOnCooldown)
                    {
                        attacksThatHitIndexes.Add(i);
                    }
                    i++;
                }
                if (attacksThatHitIndexes.Count == 0)
                {
                    if (towardsPlayerDirectionDistance > 0)
                    {
                        MoveRight();
                    }
                    else if (towardsPlayerDirectionDistance < 0)
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

}
