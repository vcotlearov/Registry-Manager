using System.Windows;
using System.Windows.Input;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for ManageRegistryKey.xaml
    /// </summary>
    public partial class ManageRegistryKey : Window
    {
        public static string RegKeyName { get; set; }
        public static int ActionFlag { get; set; }
        public ManageRegistryKey()
        {
            InitializeComponent();
            DataContext = this;
            ActionFlag = -1;
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            RegKeyName = RegKeyTextbox.Text;
            ActionFlag = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelAndCloseAction();
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CancelAndCloseAction();
            }
        }

        private void CancelAndCloseAction()
        {
            RegKeyName = string.Empty;
            ActionFlag = -1;
            this.Close();
        }
    }
}
