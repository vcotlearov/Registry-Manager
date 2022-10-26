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
        public static int ActionFlag { get; set; }
        public ManageRegistryKeyValueWindow()
        {
            InitializeComponent();
            DataContext = this;
            ActionFlag = -1;
        }
        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            RegistryKeyName = registryKeyValueNameTextbox.Text;
            RegistryKeyType = registryKeyValueTypeTextbox.Text;
            RegistryKeyValue = registryKeyValueDataTextbox.Text;
            ActionFlag = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ActionFlag = -1;
            this.Close();
        }

        private void registryKeyValueNameTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegistryKeyName))
            {
                registryKeyValueNameTextbox.CaretIndex = RegistryKeyName.Length;
            }
        }

        private void registryKeyValueDataTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegistryKeyValue))
            {
                registryKeyValueDataTextbox.CaretIndex = RegistryKeyValue.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            registryKeyValueNameTextbox.Focus();
        }
    }
}
