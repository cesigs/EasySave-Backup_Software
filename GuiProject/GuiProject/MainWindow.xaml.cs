using GuiProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace GuiProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                mutex.ReleaseMutex();
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("only one instance at a time");
                Application.Current.Shutdown();
            }
            
        }
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]

        private void LanguageSelection_Click(object sender, RoutedEventArgs e)
        {
            // Wait until click from user to select language and redirect him to functionnal page
            string lang = ((Button)sender).Tag.ToString();
            switch (lang)
            {
                case "French":
                    LangHelper.ChangeLanguage("fr");
                    Content = new FunctionalPage();
                    break;
                case "English":
                    LangHelper.ChangeLanguage("");
                    Content = new FunctionalPage();
                    break;
                default:
                    throw new NotImplementedException("");
                    break;
            }
        }

        
    }
}
