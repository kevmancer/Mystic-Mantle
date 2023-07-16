using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createtrees : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite sprite in sprites)
        {
            SpriteRenderer sr = prefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = sprite;
            Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
