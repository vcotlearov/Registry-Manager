using Registry_Manager.UI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for RecordSelection.xaml
    /// </summary>
    public partial class RecordSelection : UserControl
    {
        public ObservableCollection<RMKey> RegistryRecords { get; set; }
        public RMKey selectedRecord;
        public RecordSelection()
        {
            InitializeComponent();

            this.DataContext = this;
            RegistryRecords = new ObservableCollection<RMKey>();
        }

        public void Populate(ObservableCollection<RMKey> records)
        {
            RegistryRecords = records;
            if (records.Count > 0)
            {
                registryKeysList.SelectedItem = records[0];
            }
        }

        private void recordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                selectedRecord = e.AddedItems[0] as RMKey;
                registryKeysList.ItemsSource = selectedRecord.Parameters;
            }
        }

        private void AddRegistryKey(object sender, RoutedEventArgs e)
        {
            var modalWindow = new ManageRegistryKey();
            modalWindow.Owner = Application.Current.MainWindow;
            modalWindow.Left = modalWindow.Owner.Left + modalWindow.Owner.Width / 3;
            modalWindow.Top = modalWindow.Owner.Top + modalWindow.Owner.Height / 2;
            modalWindow.ShowDialog();

            string valueFromModalTextBox = ManageRegistryKey.RegKeyName;

            var rmKey = RegistryRecords.SingleOrDefault(x => x.Name == valueFromModalTextBox);
            if (rmKey == null && !string.IsNullOrEmpty(valueFromModalTextBox))
            {
                RegistryRecords.Add(new RMKey() { Name = valueFromModalTextBox });
                ManageRegistryKey.RegKeyName = string.Empty;
            }
        }

        private void RemoveRegistryKey(object sender, RoutedEventArgs e)
        {
            if (selectedRecord != null)
            {
                RegistryRecords.Remove(selectedRecord);
            }
        }

        private void RenameRegistryKey(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                RenameRegistryKeyWindow modalWindow = new RenameRegistryKeyWindow();
                modalWindow.Owner = Application.Current.MainWindow;
                modalWindow.Left = modalWindow.Owner.Left + modalWindow.Owner.Width / 3;
                modalWindow.Top = modalWindow.Owner.Top + modalWindow.Owner.Height / 2;
                var contextMenu = item.Parent as ContextMenu;
                var tebItem = contextMenu.PlacementTarget as TextBlock;
                RenameRegistryKeyWindow.RegistryKey = tebItem.Text;
                modalWindow.ShowDialog();

                string valueFromModalTextBox = RenameRegistryKeyWindow.RegistryKey;

                var dContext = (RMKey)item.DataContext;
                var groupTitle = dContext.Name;
                var rmKey = RegistryRecords.SingleOrDefault(x => x.Name == groupTitle);
                if (rmKey != null && !string.IsNullOrEmpty(valueFromModalTextBox))
                {
                    rmKey.Name = valueFromModalTextBox;
                }
            }
        }
    }
}
