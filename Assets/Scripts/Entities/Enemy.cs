using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject onDeathEventObject;
    private GameEvent onDeathEvent;

    protected override void Start()
    {
        base.Start();
        if (onDeathEventObject != null)
        {
            onDeathEvent = onDeathEventObject.GetComponent<GameEvent>();
        }
    }
    protected override void EntityDeath()
    {
        base.EntityDeath();
        if(onDeathEventObject != null)
        {
            onDeathEvent.TriggerEvent();
        }
        else
        {
            StartCoroutine(DestroyAfterDeath());
        }
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
