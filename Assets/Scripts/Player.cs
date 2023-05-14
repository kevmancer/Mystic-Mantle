using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool isShieldActive =false;
    protected override void EntityDeath()
    {
        base.EntityDeath();
    }

    public void AquiredPowerUp(int newMaxHealth, float newDamageReduction)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        damageReduction = newDamageReduction;
    }
}
