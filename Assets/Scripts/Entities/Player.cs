using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool isShieldActive =false;
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private IEnumerator Retry()
    {
        yield return new WaitForSeconds(2);
        gameManager.LoadLastSave();
    }

    protected override void EntityDeath()
    {
        base.EntityDeath();
        StartCoroutine(Retry());
    }

    public void AquiredPowerUp(int newMaxHealth, float newDamageReduction)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        damageReduction = newDamageReduction;
    }
}
