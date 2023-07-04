using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSensorPosition : MonoBehaviour
{
    public Vector3 positionChange;
    private Camera cameraComp;
    private ParallaxManager parallaxManager;

    // Start is called before the first frame update
    void Start()
    {
        cameraComp = gameObject.GetComponent<Camera>();
        parallaxManager = GameObject.Find("Parallax Manager").GetComponent<ParallaxManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 smoothedPos = parallaxManager.GetSmoothedPos();
        if (smoothedPos.z == 0)
        {
            transform.localPosition = new Vector3(smoothedPos.x * Constants.sizeFactor / 100, -smoothedPos.y * Constants.sizeFactor / 100, -20.264f);
        }
        else
        {
            transform.localPosition = new Vector3(smoothedPos.x * Constants.sizeFactor / 100, -smoothedPos.y * Constants.sizeFactor / 100, -smoothedPos.z * Constants.sizeFactor / 100);
        }
        
        if(smoothedPos.z == 0)
        {
            cameraComp.focalLength = 20264;
        }
        else
        {
            cameraComp.focalLength = Math.Abs(smoothedPos.z * Constants.sizeFactor * 10); 
        }
        var sensorSize = cameraComp.sensorSize;
        cameraComp.lensShift = new Vector2((-smoothedPos.x * Constants.sizeFactor / sensorSize.x) * 10, (smoothedPos.y * Constants.sizeFactor / sensorSize.y) * 10);
    }

}