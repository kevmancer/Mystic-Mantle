using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackShield : Ability
{
    private Collider2D shieldCollider;
    private Player playerEntity;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerEntity = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>();
        shieldCollider = gameObject.GetComponent<Collider2D>();
        shieldCollider.enabled = false;
    }

    protected override void OnStartExecuting()
    {
        base.OnStartExecuting();
        shieldCollider.enabled = true;
        playerEntity.isShieldActive = true;
    }

    protected override void OnStopExecuting()
    {
        base.OnStopExecuting();
        shieldCollider.enabled = false;
        playerEntity.isShieldActive = false;
    }

}
