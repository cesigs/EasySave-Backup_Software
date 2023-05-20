using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
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
using GuiProject.Language;
using System.Xml.Linq;
using GUIProject;
using GUIProject.core.Services.Strategies;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using GUIProject;
using System.Net;

namespace GuiProject.Pages
{
    /// <summary>
    /// Interaction logic for FunctionalPage.xaml
    /// </summary>
    public partial class FunctionalPage : Page
    {
        public ManualResetEvent manualResetEvent = new ManualResetEvent(true);
        public IList<Thread> threadList = new List<Thread>();
        
        public FunctionalPage()
        {
            InitializeComponent();
            
            // All the words references needed for the dictionnary
            displaySaveWorkMaj.Text = LangHelper.GetString("Display save work maj");
            displaySaveWorkMin.Content = LangHelper.GetString("Display save work min");
            addSaveWorkMin.Content = LangHelper.GetString("Add save work min");
            addSaveWorkMaj.Text = LangHelper.GetString("Add save work maj");
            name.Text = LangHelper.GetString("Name");
            id.Text = LangHelper.GetString("Id");
            source.Text = LangHelper.GetString("Source");
            saveTypeText.Text = LangHelper.GetString("Save type");
            executeAllSaveWorkMaj.Text= LangHelper.GetString("Execute all save work maj");
            executeAllSaveWorkMin.Content = LangHelper.GetString("Execute all save work min");
            executeSaveWorkMaj.Text = LangHelper.GetString("Execute save work maj");
            executeSaveWorkMin.Content = LangHelper.GetString("Execute save work min");
            complete.Content = LangHelper.GetString("Complete");
            differential.Content = LangHelper.GetString("Differential");
            destination.Text = LangHelper.GetString("Destination");
            blockingSoftware.Text = LangHelper.GetString("Blocking software");
            fileExtensionToEncrypt.Text = LangHelper.GetString("File extension to encrypt");
            fileExtensionToPrioritize.Text = LangHelper.GetString("File extension to prioritize");
            establishConnection.Content = LangHelper.GetString("Establish connection");
            fileSizeMaxi.Text = LangHelper.GetString("File size maximum");
        }


        private void LeftMenu_Click(object sender, RoutedEventArgs e)
        {
            string menuType = ((Button)sender).Tag.ToString();
            string priorityFile;
            string cryptFiles;
            string blockIfRunning;

            // If there is a button clicked, differents actions will happen
            switch (menuType)
            {
                case "DisplaySaveWorks":
                    // Display all the save work in the application
                    ServiceDB servicedb = new ServiceDB();
                    servicedb.GetAll().Clear();
                    servicedb.GenerateSaveWork();
                    ListSaveWorks.Items.Refresh();
                    ListSaveWorks.ItemsSource = servicedb.GetAll();
                    break;
                case "AddSaveWork":
                    // Add a new save work if all the fields are completed
                    DateTime now = DateTime.Now;
                    string mySaveType = "0";
                    if (saveType.SelectedIndex.ToString() == "0")
                    {
                        mySaveType = "complete";
                    }
                    else if (saveType.SelectedIndex.ToString() == "1")
                    {
                        mySaveType = "differential";
                    }

                    // If a field isn't completed, notices the user that a field is missing
                    if(saveName.Text.Length == 0 || saveSource.Text.Length == 0 || saveDest.Text.Length == 0)
                    {
                        MessageBox.Show($"{LangHelper.GetString("Field missing")}");
                    }
                    // Add the save work and execute it if no field is missing
                    else
                    {
                        
                        ServiceDB servicet = new ServiceDB();
                        servicet.GenerateSaveWork();
                        int amountSaves = servicet.GetAll().Count;
                        int newSaveId = 0;
                        if (amountSaves <= 0)
                        {
                            newSaveId = 1;
                        }
                        else
                        {
                            newSaveId = servicet.GetAll().LastOrDefault().id + 1;
                        }
                        SaveWork savework = new SaveWork
                        {
                            id = newSaveId,
                            Name = saveName.Text,
                            FileSource = saveSource.Text,
                            destPath = saveDest.Text,
                            type = mySaveType,
                            time = now.ToString()
                        };
                        if (savework != null)
                        {
                            new ServiceDB().WriteSaveWork(savework);
                        }
                        new ExecuteSaveOnCreation().ExecuteSave();
                        servicet.GetAll().Clear();
                        servicet.GenerateSaveWork();
                        ListSaveWorks.Items.Refresh();
                        ListSaveWorks.ItemsSource = servicet.GetAll();
                        MessageBox.Show($"{LangHelper.GetString("Save work added")}");
                    }
                    break;
                case "ExecuteSaveWorks":
                    // Select if there is a type of file to crypt,
                    // Select if there is a type of file to prioritize
                    // Execute all existing save work with selected parameters
                    blockIfRunning = BlockIfRunning.Text;
                    if (CryptFiles.SelectedIndex.ToString() == "1")
                    {
                        cryptFiles = ".mp4";
                    }
                    else
                    {
                        cryptFiles = "NothingToCrypt";
                    }
                    if (PrioritizeFiles.SelectedIndex.ToString() == "1")
                    {
                        priorityFile = ".txt";
                    }
                    else
                    {
                        priorityFile = "NothingToCrypt";
                    }
                    new ExecuteAllTheSaves().ExecuteSave(blockIfRunning, threadList, cryptFiles, manualResetEvent, priorityFile);
                    MessageBox.Show($"{LangHelper.GetString("Saves work launched")}");
                    break;
                case "ExecuteOneSaveWork":
                    // Execute the save work selected by the user
                    blockIfRunning = BlockIfRunning.Text;
                    string myId = saveWorkToExecuteId.Text;
                    // If the field ID isn't completed, notify the user
                    if(myId == "")
                    {
                        MessageBox.Show($"{LangHelper.GetString("Bad Id")}");
                    }
                    else
                    {
                        int intId = Int16.Parse(myId);
                        ServiceDB serviced = new ServiceDB();
                        serviced.GenerateSaveWork();
                        // If the Id is relied to a save,
                        // Select if there is a type of file to crypt,
                        // Select if there is a type of file to prioritize
                        // Execute the save work with selected parameters
                        if (intId >= serviced.GetAll().FirstOrDefault().id && intId <= serviced.GetAll().LastOrDefault().id)
                        {
                            if (CryptFiles.SelectedIndex.ToString() == "1")
                            {
                                cryptFiles = ".mp4";
                            }
                            else
                            {
                                cryptFiles = "NothingToCrypt";
                            }
                            if (PrioritizeFiles.SelectedIndex.ToString() == "1")
                            {
                                priorityFile = ".txt";
                            }
                            else
                            {
                                priorityFile = "NothingToCrypt";
                            }
                            new ExecuteOneSave().ExecuteSave(myId, blockIfRunning, threadList, cryptFiles, manualResetEvent, priorityFile);
                            MessageBox.Show($"{LangHelper.GetString("Save work launched")}");
                        }
                        else
                        {
                            MessageBox.Show($"{LangHelper.GetString("Bad Id")}");
                        }
                    }
                    break;
                case "StopSaveWorks":
                    // Stop and cancel all the save work currently executing
                    new ActionsPPS().Stop(threadList);
                    break;
                case "PauseSaveWorks":
                    // Pause all the save work currently executing
                    pauseButton.Background = Brushes.Green;
                    new ActionsPPS().Pause(manualResetEvent);
                    playButton.ClearValue(Button.BackgroundProperty);
                    break;
                case "PlaySaveWorks":
                    // Resume all the save work currently paused
                    playButton.Background = Brushes.Green;
                    new ActionsPPS().Play(manualResetEvent);
                    pauseButton.ClearValue(Button.BackgroundProperty);
                    break;
                case "Establish_Connection":
                    // Launch the server that permits the connexion with the Client
                    IPAddress myIp = IPAddress.Parse("127.0.0.1");
                    int port = 3000;
                    Server server = new Server(myIp, port);
                    server.TryConnection(server);
                    break;
                default:
                    break;
            }
        }
    }
}
