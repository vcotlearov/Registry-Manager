using System.Windows;
using System.Windows.Input;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for RenameRegistryKey.xaml
    /// </summary>
    public partial class RenameRegistryKeyWindow : Window
    {
        public static string RegistryKey { get; set; }
        public RenameRegistryKeyWindow()
        {
            InitializeComponent();
            DataContext = this;
          
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey = inputTextbox.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void inputTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegistryKey))
            {
                inputTextbox.CaretIndex = RegistryKey.Length;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            inputTextbox.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
