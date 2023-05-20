using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Console_Server
{
    public class Server
    {

        public IPAddress myIp { get; private set; }
        public int port { get; private set; }
        public bool serverStatus = true;
        private TcpListener tcpListener { get; set; }
        public Socket socketForClient { get; set; }

        public NetworkStream networkStream { get; set; }
        public StreamReader streamReader { get; set; }
        public StreamWriter streamWriter { get; set; }

        public Server(IPAddress myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;
        }

        public void startListening()
        {
            try
            {
                tcpListener = new TcpListener(myIp, port);
                tcpListener.Start();
            }
            catch
            {
                Console.WriteLine("Could not start ");
            }
        }

        public void acceptClient()
        {
            try
            {
                socketForClient = tcpListener.AcceptSocket();
            }
            catch
            {
                Console.WriteLine("Could not accept client");
            }
        }

        //allows server to exchange data with the client 

        public void clientData()
        {
            //client data
            networkStream = new NetworkStream(socketForClient);

            //allows us to read from the client 
            streamReader = new StreamReader(networkStream);

            //allows us to write to the client
            streamWriter = new StreamWriter(networkStream);
        }

        public void disconnect()
        {
            networkStream.Close();
            streamReader.Close();
            streamWriter.Close();
            socketForClient.Close();
        }

    }
}