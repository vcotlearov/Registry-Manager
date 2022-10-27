using System.Windows;

namespace Registry_Manager.UI.Windows.MessageBox
{
    /// <summary>
    /// Interaction logic for DarkMesssageBox.xaml
    /// </summary>
    public partial class DarkMesssageBox : Window
    {
        public string Text { get; set; }
        public DarkMesssageBox()
        {
            InitializeComponent();
            DataContext = this;
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
