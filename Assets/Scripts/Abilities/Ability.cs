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
    public bool isImmobilizing;
    public bool isCancelled;
    protected SpriteRenderer abilityRenderer;
    protected EntityControl parentControl;
    public AudioClip[] preSoundFx, hitSoundFx, blockSoundFx, execSoundFx;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        abilityRenderer = gameObject.GetComponent<SpriteRenderer>();
        abilityRenderer.enabled = false;
        parentControl = gameObject.transform.parent.transform.parent.GetComponent<EntityControl>();
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

    public virtual void CancelAbility()
    {
        isCancelled = true;
        isExecuting = false;
        abilityRenderer.enabled = false;
        parentControl.disableMovement = false;
    }

    public virtual void ExecuteAbility()
    {
        if (!isOnCooldown&&!isExecuting&&!isCancelled)
        {
            OnPreExecuting();
            StartCoroutine(AbilityCooldown());
            StartCoroutine(AbilityPreDelay());
        }
    }

    protected virtual IEnumerator AbilityPreDelay()
    {
        yield return new WaitForSeconds(preDelay);
        if (!isCancelled)
        {
            abilityRenderer.color = Color.white;
            isExecuting = true;
            OnStartExecuting();
            StartCoroutine(AbilityDuration());
        }
    }

    protected virtual void OnPreExecuting()
    {
        abilityRenderer.enabled = true;
        abilityRenderer.color = Color.red;
        AudioFxManager.instance.PlayAudioFxClip(preSoundFx, transform);
        isOnCooldown = true;
        if (isImmobilizing)
        {
            parentControl.disableMovement = true;
        }
    }

    protected virtual void OnStartExecuting()
    {
        AudioFxManager.instance.PlayAudioFxClip(execSoundFx, transform);
    }

    protected virtual void OnStopExecuting()
    {
        if (isImmobilizing)
        {
            parentControl.disableMovement = false;
        }
    }

    protected virtual IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(duration);
        if (!isCancelled)
        {
            abilityRenderer.enabled = false;
            isExecuting = false;
            OnStopExecuting();
        }
    }

    protected virtual IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
