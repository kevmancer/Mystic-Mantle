using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{
    public int damage;
    public List<GameObject> objectsInRangeOfAttack;
    protected GameObject parentObject;
    protected Entity parentEntity;
    public float knockBack;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        parentObject = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        parentEntity = parentObject.GetComponent<Entity>();
        objectsInRangeOfAttack = new List<GameObject>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player")&&collision.gameObject.CompareTag("Enemy"))||(!gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Player")))
        {
            objectsInRangeOfAttack.Add(collision.gameObject);
            if (isExecuting)
            {
                AttackHit(collision.gameObject);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if ((gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy")) || (!gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Player")))
        {
            objectsInRangeOfAttack.Remove(collision.gameObject);
        }
    }

    public override void ExecuteAbility(Animator entityAnimator, string triggerName)
    {
        if (!parentEntity.attackPreOrExecuting)
        {
            base.ExecuteAbility(entityAnimator, triggerName);
        }
    }

    public virtual void AttackHit(GameObject objectToAttack)
    {
        GameObject attackedObject = objectToAttack.transform.parent.gameObject;
        Entity entity = objectToAttack.transform.parent.gameObject.GetComponent<Entity>();
        bool isKnockBackLeft = parentObject.transform.position.x > attackedObject.transform.position.x;
        if (IsAttackDeflected(objectToAttack,isKnockBackLeft))
        {
            AudioFxManager.instance.PlayAudioFxClip(blockSoundFx, transform);
            entity.DamageEntity(0, knockBack, isKnockBackLeft);
        }
        else
        {
            AudioFxManager.instance.PlayAudioFxClip(hitSoundFx, transform);
            entity.DamageEntity(damage, knockBack, isKnockBackLeft);
        }
    }

    private bool IsAttackDeflected(GameObject objectToAttack, bool isAttackFromRight)
    {
        if (objectToAttack.CompareTag("Player"))
        {
            BackShield backShield = objectToAttack.transform.parent.gameObject.transform.GetChild(5).gameObject.transform.GetChild(3).GetComponent<BackShield>();
            PlayerControl playerControl = objectToAttack.transform.parent.gameObject.GetComponent<PlayerControl>();
            if (backShield.isExecuting)
            {
                if ((playerControl.isLookingRight && !isAttackFromRight) || (!playerControl.isLookingRight && isAttackFromRight))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected override void OnPreExecuting()
    {
        base.OnPreExecuting();
        parentEntity.attackPreOrExecuting = true;
    }

    protected override void OnStartExecuting()
    {
        base.OnStartExecuting();
        foreach (GameObject objectToAttack in objectsInRangeOfAttack)
        {
            AttackHit(objectToAttack);
        }
    }

    protected override void OnStopExecuting()
    {
        base.OnStopExecuting();
        parentEntity.attackPreOrExecuting = false;
    }

}
