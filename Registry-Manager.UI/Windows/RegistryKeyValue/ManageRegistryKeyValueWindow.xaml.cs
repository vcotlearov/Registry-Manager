using System.Windows;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for ManageRegistryKeyValueWindow.xaml
    /// </summary>
    public partial class ManageRegistryKeyValueWindow : Window
    {
        public static string RegistryKeyName { get; set; }
        public static string RegistryKeyType { get; set; }
        public static string RegistryKeyValue { get; set; }
        public ManageRegistryKeyValueWindow()
        {
            InitializeComponent();
        }
        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            RegistryKeyName = registryKeyValueNameTextbox.Text;
            RegistryKeyType = registryKeyValueTypeTextbox.Text;
            RegistryKeyValue = registryKeyValueDataTextbox.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registryKeyValueNameTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegistryKeyName))
            {
                registryKeyValueNameTextbox.CaretIndex = RegistryKeyName.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            registryKeyValueNameTextbox.Focus();
        }
    }
}
