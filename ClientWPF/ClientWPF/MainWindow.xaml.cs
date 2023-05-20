using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientWPF;
using ClientWPF.core;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // All the data needed to connect to the Server
        static string myIp = "127.0.0.1";
        static int port = 3000;
        public Client client = new Client(myIp, port);
        public string messageFromServer = "";
        public string messageToServer = "";
        public MainWindow()
        {
            // Try to connect to the Server
            InitializeComponent();
            client.ConnectToServer();
            MessageBox.Show("Connected to server");
            Thread.Sleep(1000);
            client.serverData();
        }
        public void Get_Messages(object sender, RoutedEventArgs e)
        {
            // Show the messages sended by the Server in a textbox
            try
            {
                messageFromServer = client.streamReader.ReadLine();
                var test = messageFromServer;
                while (messageFromServer != "")
                {
                    Retour.Text = messageFromServer;
                    messageFromServer = "";
                }
            }
            catch
            {
                MessageBox.Show($"No new message");
            }
        }
        public void LeftMenu_Click(object sender, RoutedEventArgs e)
        {
            // If a button is clicked, differents actions will happen
            string menuType = ((Button)sender).Tag.ToString();
            switch (menuType)
            {
                case "PauseSaveWorks":
                    // Pause all the save work currently executing
                    messageToServer = "Pause";
                    client.streamWriter.WriteLine($"{messageToServer}");
                    client.streamWriter.Flush();
                    break;
                case "StartSaveWorks":
                    // Resume all the save work paused
                    messageToServer = "Play";
                    client.streamWriter.WriteLine($"{messageToServer}");
                    client.streamWriter.Flush();
                    break;
                case "StopSaveWorks":
                    // Stop and cancel all the save work currently executing
                    messageToServer = "Stop";
                    client.streamWriter.WriteLine($"{messageToServer}");
                    client.streamWriter.Flush();
                    break;
            }
        }
        public void Execute_AllSaves(object sender, RoutedEventArgs e)
        {
            // Execute all the save work 
            MessageBox.Show("Execute all save work");
            messageToServer = "Execute_Save";
            client.streamWriter.WriteLine($"{messageToServer}");
            client.streamWriter.Flush();
        }
        public void ExecuteOne(object sender, RoutedEventArgs e)
        {
            // Execute the selected save work 
            MessageBox.Show("Execute selected save");
            messageToServer = "Execute_One_Save";
            client.streamWriter.WriteLine($"{messageToServer}");
            client.streamWriter.Flush();
            Thread.Sleep(4000);
            messageToServer = ID_Message.Text;
            client.streamWriter.WriteLine($"{messageToServer}");
            client.streamWriter.Flush();
        }


        public void Display(object sender, RoutedEventArgs e)
        {
            // Display all the save work in the application
            MessageBox.Show("Display Save Work");
            messageToServer = "Display";
            client.streamWriter.WriteLine($"{messageToServer}");
            client.streamWriter.Flush();
            Thread.Sleep(2000);
            messageFromServer = client.streamReader.ReadLine();
            ServiceDB servicedb = new ServiceDB();
            servicedb.GetAll().Clear();
            servicedb.GenerateSaveWork(messageFromServer);
            ListSaveWorks.Items.Refresh();
            ListSaveWorks.ItemsSource = servicedb.GetAll();
        }
    }
}
