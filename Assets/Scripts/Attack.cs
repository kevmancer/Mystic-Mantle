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
    private BoxCollider2D attackCollider;
    private SpriteRenderer attackRenderer;
    public bool attackExecuting,attackOnCooldown;
    private List<GameObject> objectsInRangeOfAttack;
   
    // Start is called before the first frame update
    void Start()
    {
        attackCollider = gameObject.GetComponent<BoxCollider2D>();
        attackRenderer = gameObject.GetComponent<SpriteRenderer>();
        attackRenderer.enabled = false;
        attackExecuting = false;
        attackOnCooldown = false;
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
            if (attackExecuting)
            {
                Entity entity = collision.gameObject.transform.parent.gameObject.GetComponent<Entity>();
                entity.DamageEntity(damage);
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
        if (!attackExecuting&&!attackOnCooldown)
        {
            attackOnCooldown = true;
            StartCoroutine(AttackCooldown());
            StartCoroutine(AttackPreDelay());
        }
    }

    IEnumerator AttackPreDelay()
    {
        yield return new WaitForSeconds(preDelay);
        attackRenderer.enabled = true;
        attackExecuting = true;
        foreach (GameObject objectToAttack in objectsInRangeOfAttack)
        {
            Entity entity = objectToAttack.transform.parent.gameObject.GetComponent<Entity>();
            entity.DamageEntity(damage);
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
        attackExecuting = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        attackOnCooldown = false;
    }

}
