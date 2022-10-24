using System.Windows;

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

        private void inputTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegKeyName))
            {
                RegKeyTextbox.CaretIndex = RegKeyName.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegKeyTextbox.Focus();
        }
    }
}
