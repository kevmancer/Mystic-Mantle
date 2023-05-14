using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{
    public int damage;
    public List<GameObject> objectsInRangeOfAttack;
    private GameObject parentObject;
    private Entity parentEntity;
    public float knockBack;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        parentObject = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        parentEntity = parentObject.GetComponent<Entity>();
        objectsInRangeOfAttack = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy")) || (!gameObject.transform.parent.gameObject.transform.parent.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Player")))
        {
            objectsInRangeOfAttack.Remove(collision.gameObject);

        }
    }

    public override void ExecuteAbility()
    {
        if (!parentEntity.attackPreOrExecuting)
        {
            base.ExecuteAbility();
        }
    }

    public void AttackHit(GameObject objectToAttack)
    {
        GameObject attackedObject = objectToAttack.transform.parent.gameObject;
        Entity entity = objectToAttack.transform.parent.gameObject.GetComponent<Entity>();
        bool isKnockBackLeft = parentObject.transform.position.x > attackedObject.transform.position.x;
        if (IsAttackDeflected(objectToAttack,isKnockBackLeft))
        {
            entity.DamageEntity(0, knockBack, isKnockBackLeft);
        }
        else
        {
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
                if ((playerControl.isLookingLeft && isAttackFromRight) || (!playerControl.isLookingLeft && !isAttackFromRight))
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
