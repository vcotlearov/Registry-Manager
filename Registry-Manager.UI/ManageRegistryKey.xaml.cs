using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for ManageRegistryKey.xaml
    /// </summary>
    public partial class ManageRegistryKey : Window
    {
        public static string RegKeyName { get; set; }
        public ManageRegistryKey()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            RegKeyName = RegKeyTextbox.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
