using Registry_Manager.UI.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for RecordSelection.xaml
    /// </summary>
    public partial class RecordSelection : UserControl
    {
        public List<RMRecord> registryRecords;
        public RMRecord selectedRecord;
        public RecordSelection()
        {
            InitializeComponent();
        }

        public void Populate(List<RMRecord> records)
        {
            recordsList.ItemsSource = records;
            if (records.Count > 0)
            {
                registryKeysList.SelectedItem = records[0];
            }
        }

        private void recordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecord = (e.AddedItems[0] as RMRecord);
            registryKeysList.ItemsSource = selectedRecord.Parameters;
        }
    }
}
