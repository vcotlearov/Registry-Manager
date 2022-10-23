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
        public ObservableCollection<RMKey> registryRecords { get; set; }
        public RMKey selectedRecord;
        public RecordSelection()
        {
            InitializeComponent();

            this.DataContext = this;
            registryRecords = new ObservableCollection<RMKey>();
        }

        public void Populate(ObservableCollection<RMKey> records)
        {
            registryRecords = records;
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

            var rmKey = registryRecords.SingleOrDefault(x => x.Name == valueFromModalTextBox);
            if (rmKey == null && !string.IsNullOrEmpty(valueFromModalTextBox))
            {
                registryRecords.Add(new RMKey() { Name = valueFromModalTextBox });
                ManageRegistryKey.RegKeyName = string.Empty;
            }
        }

        private void RemoveRegistryKey(object sender, RoutedEventArgs e)
        {
            if (selectedRecord != null)
            {
                registryRecords.Remove(selectedRecord);
            }
        }

    }
}
