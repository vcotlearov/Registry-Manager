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
        public RMKey selectedRecord { get; set; }
        public string KeyPath { get; set; }
        public RMGroup rmGroup { get; set; }

        public RecordSelection()
        {
            InitializeComponent();

            this.DataContext = this;
            RegistryRecords = new ObservableCollection<RMKey>();
        }

        public void Populate(RMGroup rmGroup)
        {
            RegistryRecords = rmGroup.Records;
            KeyPath = rmGroup.KeyPath;
            if (rmGroup.Records.Count > 0)
            {
                registryKeysList.SelectedItem = rmGroup.Records[0];
            }
            this.rmGroup = rmGroup;
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

            var valueFromModalTextBox = ManageRegistryKey.RegKeyName;
            var actionFlag = ManageRegistryKey.ActionFlag;

            var rmKey = RegistryRecords.SingleOrDefault(x => x.Name == valueFromModalTextBox);
            if (rmKey == null && actionFlag == 1 && !string.IsNullOrEmpty(valueFromModalTextBox))
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

        private void AddRegistryKeyValue(object sender, RoutedEventArgs e)
        {
            if(selectedRecord == null)
            {
                return;
            }
            var modalWindow = new ManageRegistryKeyValueWindow();
            modalWindow.Owner = Application.Current.MainWindow;
            modalWindow.Left = modalWindow.Owner.Left + modalWindow.Owner.Width / 3;
            modalWindow.Top = modalWindow.Owner.Top + modalWindow.Owner.Height / 3;
            modalWindow.ShowDialog();

            string registryKeyName = ManageRegistryKeyValueWindow.RegistryKeyName;
            string registryKeyType = ManageRegistryKeyValueWindow.RegistryKeyType;
            string registryKeyValue = ManageRegistryKeyValueWindow.RegistryKeyValue;

            var rmKey = selectedRecord.Parameters.SingleOrDefault(x => x.Name == registryKeyName);
            if (rmKey == null && !string.IsNullOrEmpty(registryKeyName))
            {
                selectedRecord.Parameters.Add(new RMValue() { Name = registryKeyName, Type = registryKeyType, Data= registryKeyValue });
                ManageRegistryKeyValueWindow.RegistryKeyName = string.Empty;
                ManageRegistryKeyValueWindow.RegistryKeyType = string.Empty;
                ManageRegistryKeyValueWindow.RegistryKeyValue = string.Empty;
            }
        }

        private void UpdateRegistryKeyValue(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                var contextMenu = item.Parent as ContextMenu;
                var listView = contextMenu.PlacementTarget as ListView;

                if (listView.SelectedItem is RMValue rmValue)
                {
                    var modalWindow = new ManageRegistryKeyValueWindow();
                    modalWindow.Owner = Application.Current.MainWindow;
                    modalWindow.Left = modalWindow.Owner.Left + modalWindow.Owner.Width / 3;
                    modalWindow.Top = modalWindow.Owner.Top + modalWindow.Owner.Height / 3;

                    ManageRegistryKeyValueWindow.RegistryKeyName = rmValue.Name;
                    ManageRegistryKeyValueWindow.RegistryKeyType = rmValue.Type;
                    ManageRegistryKeyValueWindow.RegistryKeyValue = rmValue.Data;
                    modalWindow.ShowDialog();

                    if(ManageRegistryKeyValueWindow.ActionFlag == 1)
                    {
                        string registryKeyName = ManageRegistryKeyValueWindow.RegistryKeyName;
                        string registryKeyType = ManageRegistryKeyValueWindow.RegistryKeyType;
                        string registryKeyValue = ManageRegistryKeyValueWindow.RegistryKeyValue;

                        var rmKeyValue = selectedRecord.Parameters.SingleOrDefault(x => x.Name == rmValue.Name && x.Data == rmValue.Data);
                        if (rmKeyValue != null && !string.IsNullOrEmpty(registryKeyName))
                        {
                            rmKeyValue.Name = registryKeyName;
                            rmKeyValue.Data = registryKeyValue;

                            ManageRegistryKeyValueWindow.RegistryKeyName = string.Empty;
                            ManageRegistryKeyValueWindow.RegistryKeyType = string.Empty;
                            ManageRegistryKeyValueWindow.RegistryKeyValue = string.Empty;
                        }
                    }
                    
                }
            }
        }

        private void RemoveRegistryKeyValue(object sender, RoutedEventArgs e)
        {
            if (registryKeysList.SelectedItem is RMValue keyValue)
            {
                selectedRecord.Parameters.Remove(keyValue);
            }
        }

        private void registryKeyPathTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(registryKeyPathTextbox.Text))
            {
                rmGroup.KeyPath = registryKeyPathTextbox.Text;
            }
        }
    }
}
