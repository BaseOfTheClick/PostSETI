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

    public Net.Connector socket = null;
    private int i = 0;

    // Use this for initialization
    void Start()
    {
        socket = new Net.Connector("np.nixcode.us", 31337);
        socket.write("Login:" + "Kevin" + "\n");
        string reply = socket.readChunk(512);
    }

    // Update is caleled once per frame
    void Update()
    {

        if (i++ == 10)
        {
            i = 0;
            socket.write("Year:"
                + GameObject.Find("_GameManager").GetComponent<YearCounter>().currentYear.ToString() + "\n");
            string text = socket.readChunk(256);
            gameObject.GetComponent<Text>().text = text;
        }

    }
}
