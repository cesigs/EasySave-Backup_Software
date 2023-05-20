using System;
using System.IO;
using System.Net;
using GuiProject;

namespace Console_Server


// NE PAS TERMINER LE PROGRAMME SI DECO -> faire une boucle qui vérifie que le client est connecté ? / faire une fonction qui vérifie la connexion ? 


// recuperer message de vincent, et en fonction du message j'execute une fonction
// switch case : if je recois ça j'execute la fonction liée 

// ex : executesave (parametres etc) -> fonction publique donc pas de soucis 

//faire des boutons sur partie client qui vont renvoyer des textes -> pas de fautes de syntaxe etc pour le server 

//probleme avec le retour, vinz m'envoie un message vide car il recoit mon message
//et renvoi INSTANTANEMENT le contenu de la text box, qui est vide, trop RAPIDE 


// faire un try et catch a la place du while ? try message exit et catch le reste du prog ? 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SERVER";

            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            int port = 3000;

            Server server = new Server(myIp, port);


            //
            server.startListening();
            Console.WriteLine("Server started");

            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Waiting for connection");

            //if somebody wants to connect, accept it
            server.acceptClient();
            Console.WriteLine("Client connected");

            string messageFromClient = "";
            string messageToClient = "";

            try
            {
                server.clientData();

                //while the server is on 
                while (server.serverStatus)
                {

                    //only when a client is connected
                    if (server.socketForClient.Connected)
                    {
                        //we are expecting a message from the client
                        messageFromClient = server.streamReader.ReadLine();
                        //send the message written by the client
                        Console.WriteLine($"Client : {messageFromClient}");

                        //if the client says goodbye
                        if (messageFromClient == "exit")
                        {
                            server.serverStatus = false; //break the loop

                            //stop writing and reading from the client 
                            server.streamReader.Close();
                            server.streamWriter.Close();
                            server.networkStream.Close();
                            return;
                        }

                        else if (messageFromClient == "Connection succesfull")
                        {
                            Console.WriteLine("Client connected to server");
                        }

                        else if (messageFromClient == "Display save work")
                        {
                            Console.WriteLine("Display save work to client");
                            

                            // comment faire ça ? on peut envoyer que du texte par le stream ? 
                        }

                        else if (messageFromClient == "Execute all save work")
                        {
                            Console.WriteLine("Execute all save work");
                        }


                        //envoyer des elements serialiés : 
                        // avant d'envoyer un element du client au server, on faisait un json.convert.serializeObject(aswer)
                        // on serialise forcement nos objets donc parfait 
                        // pou r les save work, on fait appel a display save work, dans service db
                        // on va ne pas deserialises les 
                        // encode byte par byte 


                        /*
                        else if (messageFromClient == $"Execute {id} save work")
                        {
                            Console.WriteLine($"Executing {id} save work");
                        }
                        */


                        /* ENCAPSULE DANS DES FONCTIONS ?
                        else if (messageFromClient == "Button play")
                        {
                            Console.WriteLine("Button play");
                        }

                        else if (messageFromClient == "Button pause")
                        {
                            Console.WriteLine("Button pause");
                        }

                        else if (messageFromClient == "Button stop")
                        {
                            Console.WriteLine("Button stop");
                        }

                        */

                        //if the client said something, it's my turn now
                        Console.Write("Server : ");
                        //read from my console
                        messageToClient = Console.ReadLine();
                        //send message to the client
                        server.streamWriter.WriteLine(messageToClient);

                        //clear the buffer, when data is exchanged between process the buffer stores that 
                        //data temporaly so everytime we write we need to clear the buffer
                        server.streamWriter.Flush();
                    }
                }

                //after the client says exit, disconnect
                Console.WriteLine("Client disconnected");
                server.disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}