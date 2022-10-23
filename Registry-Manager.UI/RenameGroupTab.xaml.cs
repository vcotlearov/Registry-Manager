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
    /// Interaction logic for RenameGroupTab.xaml
    /// </summary>
    public partial class RenameGroupTab : Window
    {
        public static string groupName { get; set; }
        public RenameGroupTab()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            groupName = groupNameTextbox.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
