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
    protected EntityControl parentControl;
    public AudioClip[] preSoundFx, hitSoundFx, blockSoundFx, execSoundFx;
    protected Animator animator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        parentControl = gameObject.transform.parent.transform.parent.GetComponent<EntityControl>();
        animator = gameObject.GetComponent<Animator>();
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
        parentControl.disableMovement = false;
    }

    public virtual void ExecuteAbility(Animator entityAnimator,string triggerName)
    {
        if (!isOnCooldown&&!isExecuting&&!isCancelled)
        {
            entityAnimator.SetTrigger(triggerName);
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
            animator.SetTrigger("attackExecuted");
            isExecuting = true;
            OnStartExecuting();
            StartCoroutine(AbilityDuration());
        }
    }

    protected virtual void OnPreExecuting()
    {
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
