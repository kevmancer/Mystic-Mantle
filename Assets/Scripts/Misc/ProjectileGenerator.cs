using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : Ability
{
    public GameObject projectilePrefab;
    protected override void Start()
    {
        base.Start();
        damageDuration = 0f;
    }
    protected override void OnStartExecuting()
    {
        base.OnStartExecuting();
        Instantiate(projectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
