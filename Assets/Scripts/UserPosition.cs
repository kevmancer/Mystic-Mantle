using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class UserPosition : MonoBehaviour
{
    public Vector3 positionChange;
    Thread receiveThread;
    UdpClient client; 
    int port;
    public Camera camera;
    private float distance, x_movement, y_movement;
    private string sendText = "empty_string";
    
    // Start is called before the first frame update
    void Start()
    {
        port = 5065;

        InitUDP();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-x_movement/100, -y_movement/100, distance/100);
        camera.focalLength = distance*10;
        var sensorSize = camera.sensorSize;
        camera.lensShift = new Vector2((-x_movement/sensorSize.x)*10,(y_movement/sensorSize.y)*10);

        if (x_movement > 10)
        {
            sendText = "haahahah";
        }
    }

    private void InitUDP()
    {
        print ("UDP Initialized");

        receiveThread = new Thread (new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }
    
    private void ReceiveData()
    {
        client = new UdpClient (port); 
        while (true) 
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                byte[] data = client.Receive(ref anyIP); 

                string text = Encoding.UTF8.GetString(data);

                x_movement = float.Parse(text.Split(',')[0]);
                y_movement = float.Parse(text.Split(',')[1]);
                distance = float.Parse(text.Split(',')[2]);

                if (sendText != String.Empty)
                {
                    print("sent"+sendText);
                    var dataToSend = Encoding.ASCII.GetBytes(sendText);
                    client.Send(dataToSend, dataToSend.Length, anyIP);
                    sendText = "empty_string";
                }
            } 
            catch(Exception e)
            {
                print (e.ToString());
            }
        }
    }
}