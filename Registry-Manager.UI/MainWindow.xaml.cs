using Registry_Manager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;

namespace Registry_Manager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, string> randomizer = new Dictionary<int, string>()
        {
            {1, "diamond" },
            {2, "fishing" },
            {3, "winner" },
            {4, "idea" },
            {5, "audience" },
            {6, "revenue" },
            {7, "article" },
            {8, "explanation" },
            {9, "marketing" },
            {10, "insect" },
            {11, "departure" },
            {12, "data" },
            {13, "way" },
            {14, "administration" },
            {15, "power" },
            {16, "world" },
            {17, "grandmother" },
            {18, "signature" },
            {19, "charity" },
            {20, "wood" },
        };
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;
        private string addTabHeader =  "+";

        public RMConfig rmConfig = null;

        public MainWindow()
        {

            try
            {
                InitializeComponent();

                InitializeDefaultConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeDefaultConfig()
        {
            // initialize tabItem array
            _tabItems = new List<TabItem>();

            InitializeAddTab();

            RMGroup group = new RMGroup();
            group.Name = "Group";
            // add first tab
            this.AddTabItem(group);

            // bind tab control
            tabDynamic.DataContext = _tabItems;
            tabDynamic.SelectedIndex = 0;
        }

        private void InitializeAddTab()
        {
            // add a tabItem with + in header 
            TabItem tabAdd = new TabItem();
            tabAdd.Header = addTabHeader;
            _tabItems.Add(tabAdd);
        }

        private TabItem AddTabItem(RMGroup group)
        {
            try
            {
                int count = _tabItems.Count;

                // create new tab item
                TabItem tab = new TabItem();
                tab.Header = group.Name;
                tab.Name = string.Format("tab{0}", count);
                tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;

                RecordSelection recordSelection = new RecordSelection();
                recordSelection.Populate(group.Records);
                tab.Content = recordSelection;
                // insert tab item right before the last (+) tab item
                _tabItems.Insert(count - 1, tab);
                return tab;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;

            if (tab != null && tab.Header != null)
            {
                if (tab.Header.Equals(addTabHeader))
                {
                    // clear tab control binding
                    tabDynamic.DataContext = null;


                    // add new tab
                    RMGroup group = new RMGroup();
                    group.Name = "New Group";
                    TabItem newTab = this.AddTabItem(group);

                    // bind tab control
                    tabDynamic.DataContext = _tabItems;

                    // select newly added tab item
                    tabDynamic.SelectedItem = newTab;
                }
                else
                {
                    // your code here...
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = tabDynamic.Items.Cast<TabItem>().Where
                       (i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (_tabItems.Count < 3)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else if (MessageBox.Show(string.Format
                ("Are you sure you want to remove the tab '{0}'?", tab.Header.ToString()),
                    "Remove Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;

                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    _tabItems.Remove(tab);

                    // bind tab control
                    tabDynamic.DataContext = _tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = _tabItems[0];
                    }
                    tabDynamic.SelectedItem = selectedTab;
                }
            }
        }

     

        private void NewConfig_Click(object sender, RoutedEventArgs e)
        {
            InitializeDefaultConfig();
        }
        private void OpenConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                var jsonConfig = File.ReadAllText(openFileDialog.FileName);
                var config = JsonConvert.DeserializeObject<RMConfig>(jsonConfig);

                ImportConfig(config);
            }
        }

        private void ImportConfig(RMConfig config)
        {
            _tabItems = new List<TabItem>();

            InitializeAddTab();

            foreach (var group in config.Groups)
            {
                this.AddTabItem(group);
            }

            // bind tab control
            tabDynamic.DataContext = _tabItems;
            tabDynamic.SelectedIndex = 0;
        }

        private void SaveAsConfig_Click(object sender, RoutedEventArgs e)
        {
            var jsonConfig = JsonConvert.SerializeObject(rmConfig);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, jsonConfig);
        }

        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
