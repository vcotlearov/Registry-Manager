using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create the startup window
            mainWindow = new MainWindow();
            // Show the window
            mainWindow.Show();
        }

        private void AppMinimize_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
