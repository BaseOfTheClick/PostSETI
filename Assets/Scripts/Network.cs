using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Net
{
    public class Connector
    {
        TcpClient client = null;
        NetworkStream stream = null;
        string serverHost = string.Empty;
        int serverPort;
        Byte[] chunk = null;

        public Connector(string host, int port)
        {
            if (!connect(host, port))
            {
                Console.WriteLine("Net::Connector(host, port) unable "
                    + "to connect to " + host + ":" + port);
            }
        }

        public bool connect(string host = null, int port = 0)
        {
            if (host != null)
            {
                serverHost = host;
                serverPort = port;
            }

            if (serverHost == null)
                return false;

            client = new TcpClient(serverHost, serverPort);
            stream = client.GetStream();

            return true;
        }

        public string readChunk(int size)
        {
            chunk = new Byte[size];
            Int32 bytes = stream.Read(chunk, 0, size - 1);
            if (bytes <= 0)
                return "";

            string temp = System.Text.Encoding.ASCII.GetString(chunk);
            return temp;
        }

        public void write(string text)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

        public void close()
        {
            stream.Close();
            client.Close();
        }

    };
};

public class Network : MonoBehaviour
{

    private Net.Connector socket = null;
    private const string HOST = "np.nixcode.us";
    private const int PORT = 31337;

    // Counter and limit
    private int i = 0;
    private const int FRAME_LIMIT = 20;

    private string name = string.Empty;

    // Use this for initialization
    void Start()
    {
        // Do nothing, use Awake()
    }

    void Awake()
    {
        name = Login.playerName;
        if(name == null)
        {
            Debug.Log("Login script was not in a persistent state");
            return;
        }

        try {
            socket = new Net.Connector(HOST, PORT);
        } catch {
            Debug.Log("Net.Connector(" + HOST + ", " + PORT.ToString()
                      + ") failed to initialize");
            return;
        }
        socket.write("Login:" + name + "\n");
        string reply = socket.readChunk(512);
    }

    // Update is called once per frame
    void Update()
    {
        if(++i != FRAME_LIMIT)
            return;

        GameObject obj = GameObject.Find("_GameManager");
        string yr = obj.GetComponent<YearCounter>().currentYear.ToString();
        socket.write("Year:" + yr + "\n");
        gameObject.GetComponent<Text>().text = socket.readChunk(256);
        i = 0;

    }

}
