using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using GuiProject;
using GUIProject;
using System.Threading;
using GUIProject.core.Services.Strategies;
using Newtonsoft.Json;
using System.Text;

namespace GuiProject
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
        public ManualResetEvent manualResetEvent = new ManualResetEvent(true);
        public IList<Thread> threadList = new List<Thread>();
        public Server(IPAddress myIp, int port)
        {
            this.myIp = myIp;
            this.port = port;
        }

        public void TryConnection(Server server)
        {
            server.startListening();
            server.acceptClient();
            string messageFromClient = "";
            string messageToClient = "";
            listeningClient(server, messageFromClient);
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

        public void listeningClient(Server server, string messageFromClient)
        {
            try
            {
                server.clientData();
                string messageToClient = "";
                string priorityFile = "";
                string cryptFiles = "";

                while (server.serverStatus)
                {
                    if (server.socketForClient.Connected)
                    {
                        messageFromClient = server.streamReader.ReadLine();
                        switch (messageFromClient)
                        {
                            case "Execute_Save" :
                                server.streamWriter.WriteLine($"Executing save");
                                server.streamWriter.Flush();
                                string blockIfRunningAll = "";
                                cryptFiles = "NothingToCrypt";
                                new ExecuteAllTheSaves().ExecuteSave(blockIfRunningAll, threadList, cryptFiles, manualResetEvent, priorityFile);
                                break;

                            case "Execute_One_Save":
                                Thread.Sleep(8000);
                                string myId = streamReader.ReadLine();
                                server.streamWriter.WriteLine($"Executing save {myId}");
                                server.streamWriter.Flush();
                                string blockIfRunningOne = "";
                                cryptFiles = "NothingToCrypt";
                                int intId = Int16.Parse(myId);
                                ServiceDB serviced = new ServiceDB();
                                serviced.GenerateSaveWork();
                                if (intId >= serviced.GetAll().FirstOrDefault().id && intId <= serviced.GetAll().LastOrDefault().id)
                                {
                                    new ExecuteOneSave().ExecuteSave(myId, blockIfRunningOne, threadList, cryptFiles, manualResetEvent, priorityFile);
                                    server.streamWriter.WriteLine($"Save work {myId} executing");
                                    server.streamWriter.Flush();
                                }
                                else
                                {
                                    server.streamWriter.WriteLine("Bad ID");
                                    server.streamWriter.Flush();
                                }
                                break;

                            case "Play":
                                manualResetEvent.Set();
                                server.streamWriter.WriteLine($"Save played");
                                server.streamWriter.Flush();
                                break;

                             case "Pause":
                                manualResetEvent.Reset();
                                server.streamWriter.WriteLine($"Save paused");
                                server.streamWriter.Flush();
                                break;

                            case "Stop":
                                new ActionsPPS().Stop(threadList);
                                server.streamWriter.WriteLine($"Save stopped");
                                server.streamWriter.Flush();
                                break;
                            case "Display":
                                ServiceDB servicedb = new ServiceDB();
                                servicedb.GetAll().Clear();
                                servicedb.GenerateSaveWork();
                                servicedb.GetAll();

                                string serializedMessage = JsonConvert.SerializeObject(servicedb.GetAll());
                                server.streamWriter.WriteLine(serializedMessage);
                                server.streamWriter.Flush();
                               
                                break;
                        }
                    }
                }
            }
            catch
            {
                server.disconnect();
            }


        }

    }
}