using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager Instance { get; private set; }
    private Thread receiveThread;
    private UdpClient client;
    private int port;
    public Vector3 facePosition { get; private set; } = new Vector3(0, 0, 0);
    public string sendText = "empty_string";

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        port = 5065;
        InitUDP();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitUDP()
    {
        print("UDP Initialized");

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

                facePosition = new Vector3(float.Parse(text.Split(',')[0]), float.Parse(text.Split(',')[1]), float.Parse(text.Split(',')[2]));

                if (sendText != string.Empty)
                {
                    print("sent" + sendText);
                    var dataToSend = Encoding.ASCII.GetBytes(sendText);
                    client.Send(dataToSend, dataToSend.Length, anyIP);
                    sendText = "empty_string";
                }
            }
            catch (Exception e)
            {
                print(e.ToString());
            }
        }
    }

}
