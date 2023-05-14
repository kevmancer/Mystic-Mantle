using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player").gameObject;
    }

    private void LateUpdate()
    {
        gameObject.transform.position = new Vector3(playerObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

}
