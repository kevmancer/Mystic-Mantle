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
    public Sprite playerPowerUpSprite;
    public bool spawnsBoss=false;
    public GameObject bossPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControl playerControl = collision.gameObject.transform.parent.gameObject.GetComponent<PlayerControl>();
            Player playerEntity = collision.gameObject.transform.parent.gameObject.GetComponent<Player>();
            playerControl.AquiredPowerUp(speed, jumpForce, attacksPrefab, playerPowerUpSprite);
            playerEntity.AquiredPowerUp(maxHealth, damageReduction);
            if (spawnsBoss)
            {
                Instantiate(bossPrefab, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y + 2, gameObject.transform.position.z), gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
