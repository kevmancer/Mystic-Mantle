using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float preDelay;
    public float duration;
    public int damage;
    public bool hasDuration;
    public bool isFromPlayer;
    public float cooldown;
    private SpriteRenderer attackRenderer;
    public bool attackOnCooldown, attackDamaging;
    private List<GameObject> objectsInRangeOfAttack;
    private Entity parentEntity;
    private EntityControl parentEntityControl;
    public float knockBack;
   
    // Start is called before the first frame update
    void Start()
    {
        attackRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentEntity = gameObject.transform.parent.gameObject.GetComponent<Entity>();
        parentEntityControl = gameObject.transform.parent.gameObject.GetComponent<EntityControl>();
        attackRenderer.enabled = false;
        attackOnCooldown = false;
        attackDamaging = false;
        objectsInRangeOfAttack = new List<GameObject>();
        if (cooldown < preDelay + duration)
        {
            cooldown = preDelay + duration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isFromPlayer&&collision.gameObject.CompareTag("Enemy"))||(!isFromPlayer && collision.gameObject.CompareTag("Player")))
        {
            objectsInRangeOfAttack.Add(collision.gameObject);
            if (attackDamaging)
            {
                Entity entity = collision.gameObject.transform.parent.gameObject.GetComponent<Entity>();
                entity.DamageEntity(damage, knockBack, parentEntityControl.isLookingLeft);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((isFromPlayer && collision.gameObject.CompareTag("Enemy")) || (!isFromPlayer && collision.gameObject.CompareTag("Player")))
        {
            objectsInRangeOfAttack.Remove(collision.gameObject);

        }
    }

    public void ExecuteAttack()
    {
        if (!parentEntity.attackExecuting&&!attackOnCooldown)
        {
            parentEntity.attackExecuting = true;
            attackOnCooldown = true;
            StartCoroutine(AttackCooldown());
            StartCoroutine(AttackPreDelay());
        }
    }

    IEnumerator AttackPreDelay()
    {
        yield return new WaitForSeconds(preDelay);
        attackRenderer.enabled = true;
        attackDamaging = true;
        foreach (GameObject objectToAttack in objectsInRangeOfAttack)
        {
            Entity entity = objectToAttack.transform.parent.gameObject.GetComponent<Entity>();
            entity.DamageEntity(damage, knockBack, parentEntityControl.isLookingLeft);
        }
        if (hasDuration)
        {
            StartCoroutine(AttackDuration());
        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(duration);
        attackRenderer.enabled = false;
        attackDamaging = false;
        parentEntity.attackExecuting = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        attackOnCooldown = false;
    }

}
