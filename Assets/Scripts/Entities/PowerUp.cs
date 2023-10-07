using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 15;
    public float jumpForce = 850;
    public int maxHealth = 150;
    public float damageReduction = 0.3F;
    public GameObject attacksPrefab;
    public RuntimeAnimatorController playerPowerUpAnimator;
    public bool spawnsBoss=false;
    public GameObject bossPrefab;
    public ParticleSystem particle;
    private GameObject graphic;
    private bool isUsed = false;
    public AudioClip[] sfx;
    public GameObject onAcquireEventObject;
    private GameEvent onAcquireEvent;

    // Start is called before the first frame update
    void Start()
    {
        graphic = GameObject.Find("GFX").gameObject;
        if (onAcquireEventObject != null)
        {
            onAcquireEvent = onAcquireEventObject.GetComponent<GameEvent>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && !isUsed)
        {
            PlayerControl playerControl = collision.gameObject.transform.parent.gameObject.GetComponent<PlayerControl>();
            Player playerEntity = collision.gameObject.transform.parent.gameObject.GetComponent<Player>();
            if (playerEntity.isAlive)
            {
                playerControl.AquiredPowerUp(speed, jumpForce, attacksPrefab, playerPowerUpAnimator);
                playerEntity.AquiredPowerUp(maxHealth, damageReduction);
                if (spawnsBoss)
                {
                    Instantiate(bossPrefab, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y + 2, gameObject.transform.position.z), gameObject.transform.rotation);
                }
                particle.Play();
                AudioFxManager.instance.PlayAudioFxClip(sfx, transform);
                isUsed = true;
                graphic.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.GetComponent<Light>().enabled = false;
                if (onAcquireEventObject != null)
                {
                    onAcquireEvent.TriggerEvent();
                }
                StartCoroutine(WaitForParticles());
            }
        }else if (isUsed)
        {
            graphic.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.GetComponent<Light>().enabled = false;
        }
    }

    private IEnumerator WaitForParticles()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
