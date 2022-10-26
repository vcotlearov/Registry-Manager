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

namespace Registry_Manager.UI.Windows.MessageBox
{
    /// <summary>
    /// Interaction logic for DarkMesssageBox.xaml
    /// </summary>
    public partial class DarkMesssageBox : Window
    {
        public DarkMesssageBox()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnConfirm.Focus();
        }
    }
}
