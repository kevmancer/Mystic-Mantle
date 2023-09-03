using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // time between ability trigger and animation start playing
    public float preDelay;
    // time between animation start and damage/effenct happening
    public float animationPreDelay;
    public float damageDuration;
    private float animationDuration;
    private float abilityDuration;
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
        if (animationDuration < animationPreDelay + damageDuration)
        {
            animationDuration = animationPreDelay + damageDuration;
        }
        if (cooldown < preDelay + damageDuration)
        {
            cooldown = preDelay + damageDuration;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Effect idle":
                    break;
                default:
                    animationDuration = clip.length;
                    break;
            }
        }
        abilityDuration = preDelay + animationDuration;
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
            StartCoroutine(AbilityPreDelay());
            OnPreExecuting();
            StartCoroutine(AbilityCooldown());
            if (isImmobilizing)
            {
                parentControl.disableMovement = true;
            }
            StartCoroutine(AbilityDuration());
        }
    }

    protected virtual IEnumerator AbilityPreDelay()
    {
        yield return new WaitForSeconds(preDelay);
        if (!isCancelled)
        {
            animator.SetTrigger("attackExecuted");
            StartCoroutine(AbilityAnimationPreDelay());
        }
    }

    protected virtual IEnumerator AbilityAnimationPreDelay()
    {
        yield return new WaitForSeconds(animationPreDelay);
        if (!isCancelled)
        {
            isExecuting = true;
            OnStartExecuting();
            StartCoroutine(AbilityDamageDuration());
        }
    }

    protected virtual void OnPreExecuting()
    {
        AudioFxManager.instance.PlayAudioFxClip(preSoundFx, transform);
        isOnCooldown = true;
    }

    protected virtual void OnStartExecuting()
    {
        AudioFxManager.instance.PlayAudioFxClip(execSoundFx, transform);
    }

    protected virtual void OnStopExecuting()
    {
        
    }

    protected virtual IEnumerator AbilityDamageDuration()
    {
        yield return new WaitForSeconds(damageDuration);
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

    protected virtual IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(abilityDuration);
        if (isImmobilizing)
        {
            parentControl.disableMovement = false;
        }
        OnAbilityFinished();
    }

    protected virtual void OnAbilityFinished()
    {

    }
}
