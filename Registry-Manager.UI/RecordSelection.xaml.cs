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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for RecordSelection.xaml
    /// </summary>
    public partial class RecordSelection : UserControl
    {
        public List<RegistryRecord> registryRecords;
        public RegistryRecord selectedRecord;
        public RecordSelection()
        {
            InitializeComponent();

            registryRecords = new List<RegistryRecord>();

            for (int i = 0; i < 20; i++)
            {
                RegistryRecord registryRecord = new RegistryRecord();
                registryRecord.Title = "Record title " + i;
                registryRecord.RegistryKeys = new List<RegistryKey>();
                registryRecord.RegistryKeys.Add(new RegistryKey() { Key = "LastAttemptStatus", Type="REG_DWORD", Value = "0" });
                registryRecord.RegistryKeys.Add(new RegistryKey() { Key = "LastAttemptVersion", Type="REG_DWORD", Value = "1b8" });
                registryRecord.RegistryKeys.Add(new RegistryKey() { Key = "LowestSupportedVersion", Type="REG_DWORD", Value = "1b8" });
                registryRecord.RegistryKeys.Add(new RegistryKey() { Key = "Type", Type="REG_DWORD", Value = "1" });
                registryRecord.RegistryKeys.Add(new RegistryKey() { Key = "Version", Type="REG_DWORD", Value = "1b8" });
                registryRecords.Add(registryRecord);
                
                if (selectedRecord == null)
                {
                    selectedRecord = registryRecord;
                }
            }

            recordsList.ItemsSource = registryRecords;
        }

        private void recordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            registryKeysList.ItemsSource = selectedRecord.RegistryKeys;
        }
    }
    public class RegistryRecord
    {
        public string Title { get; set; }
        public string Template { get; set; }
        public List<RegistryKey> RegistryKeys { get; set; }
    }

    public class RegistryKey
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
