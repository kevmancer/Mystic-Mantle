using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    private Vector3 direction;
    public float speed;
    public float knockBack;
    public AudioClip[] hitSoundFx, blockSoundFx, missSoundFx;

    void Start()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        direction = (player.transform.position - gameObject.transform.position).normalized;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackHit(collision.gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            AttackMissed();
        }
    }

    public virtual void AttackMissed()
    {
        AudioFxManager.instance.PlayAudioFxClip(missSoundFx, transform);
        Destroy(gameObject);
    }

    public virtual void AttackHit(GameObject objectToAttack)
    {
        GameObject attackedObject = objectToAttack.transform.parent.gameObject;
        Entity entity = objectToAttack.transform.parent.gameObject.GetComponent<Entity>();
        bool isKnockBackLeft = direction.x < 0;
        if (IsAttackDeflected(objectToAttack, isKnockBackLeft))
        {
            AudioFxManager.instance.PlayAudioFxClip(blockSoundFx, transform);
            entity.DamageEntity(0, knockBack, isKnockBackLeft);
        }
        else
        {
            AudioFxManager.instance.PlayAudioFxClip(hitSoundFx, transform);
            entity.DamageEntity(damage, knockBack, isKnockBackLeft);
        }
        Destroy(gameObject);
    }

    private bool IsAttackDeflected(GameObject objectToAttack, bool isKnockBackLeft)
    {
        if (objectToAttack.CompareTag("Player"))
        {
            BackShield backShield = objectToAttack.transform.parent.gameObject.transform.GetChild(5).gameObject.transform.GetChild(3).GetComponent<BackShield>();
            PlayerControl playerControl = objectToAttack.transform.parent.gameObject.GetComponent<PlayerControl>();
            if (backShield.isExecuting)
            {
                if ((playerControl.isLookingRight && !isKnockBackLeft) || (!playerControl.isLookingRight && isKnockBackLeft))
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

}
