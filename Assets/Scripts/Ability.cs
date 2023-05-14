using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float preDelay;
    public float duration;
    public float cooldown;
    public bool isExecuting;
    public bool isOnCooldown;
    private SpriteRenderer abilityRenderer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        abilityRenderer = gameObject.GetComponent<SpriteRenderer>();
        abilityRenderer.enabled = false;
        isOnCooldown = false;
        isExecuting = false;
        if (cooldown < preDelay + duration)
        {
            cooldown = preDelay + duration;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void ExecuteAbility()
    {
        if (!isOnCooldown&&!isExecuting)
        {
            isOnCooldown = true;
            StartCoroutine(AbilityCooldown());
            StartCoroutine(AbilityPreDelay());
        }
    }

    protected virtual IEnumerator AbilityPreDelay()
    {
        yield return new WaitForSeconds(preDelay);
        abilityRenderer.enabled = true;
        isExecuting = true;
        OnStartExecuting();
        StartCoroutine(AbilityDuration());
    }

    protected virtual void OnStartExecuting()
    {

    }

    protected virtual void OnStopExecuting()
    {

    }

    protected virtual IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(duration);
        abilityRenderer.enabled = false;
        isExecuting = false;
        OnStopExecuting();
    }

    protected virtual IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
