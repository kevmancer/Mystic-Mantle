using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    private Entity player;
    private Image hpBar;
    private RectTransform theBarRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Entity>();
        hpBar = gameObject.GetComponent<Image>();
        theBarRectTransform = hpBar.transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        float width = ((float)player.currentHealth / (float)player.maxHealth) * 100F;
        theBarRectTransform.sizeDelta = new Vector2(width, theBarRectTransform.sizeDelta.y);
    }
}
