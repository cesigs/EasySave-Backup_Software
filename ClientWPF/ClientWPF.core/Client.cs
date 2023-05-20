using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ClientWPF.core
{
    public class Client
    {
        public string myIp { get; private set; }
        public int port { get; set; }
        private TcpClient socketForServer;
        public bool clientStatus = true;
        public NetworkStream networkStream { get; set; }
        public StreamWriter streamWriter { get; set; }
        public StreamReader streamReader { get; set; }
        public Client(string myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;
        }

        public void ConnectToServer()
        {
            // Select the Ip and port used for the connection with the server
            try
            {
                socketForServer = new TcpClient(myIp.ToString(), port);
            }
            catch
            {

            }
        }
        
        public void serverData()
        {
            // Methods needed to read and write in the networkStream
            networkStream = socketForServer.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }

        public void disconnect()
        {
            streamWriter.Close();
            streamReader.Close();
            socketForServer.Close();
            networkStream.Close();
        }
    }
}