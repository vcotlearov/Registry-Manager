using Registry_Manager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private void OpenConfig()
        {

            rmConfig = new RMConfig();
            RMTemplate rMTemplate = new RMTemplate();
            for (int i = 0; i < 3; i++)
            {
                RMParameter rmTemplateParameter = new RMParameter();
                rmTemplateParameter.Name = $"Name {i}";
                rmTemplateParameter.Type = $"Type {i}";
                rmTemplateParameter.Data = $"Data {i}";
                rMTemplate.Parameters.Add(rmTemplateParameter);
            }
            rmConfig.Templates.Add(rMTemplate);

            Random r = new Random();
            int tabCounter = r.Next(2, 5);
            for (int j = 0; j < tabCounter; j++)
            {
                RMGroup rMGroup = new RMGroup();
                rMGroup.Name = $"Group {j}";
                int recordCounter = r.Next(3, 20);
                for (int x = 1; x <= recordCounter; x++)
                {
                    RMRecord rMRecord = new RMRecord();
                    rMRecord.Name = randomizer[r.Next(1, 20)];
                    rMRecord.TemplateName = rMTemplate.Name;

                    int paramCounter = r.Next(1, 6);
                    for (int y = 0; y < paramCounter; y++)
                    {
                        RMParameter rMParameter = new RMParameter();
                        rMParameter.Name = $"Param {y}";
                        rMParameter.Type = $"Type {y}";
                        rMParameter.Data = $"Data {y}";
                        rMRecord.Parameters.Add(rMParameter);
                    }

                    rMGroup.Records.Add(rMRecord);
                }
                rmConfig.Groups.Add(rMGroup);
            }

            _tabItems = new List<TabItem>();

            // add a tabItem with + in header 
            TabItem tabAdd = new TabItem();
            tabAdd.Header = addTabHeader;

            _tabItems.Add(tabAdd);

            foreach(var group in rmConfig.Groups)
            {
                this.AddTabItem(group);
            }

            // bind tab control
            tabDynamic.DataContext = _tabItems;

            tabDynamic.SelectedIndex = 0;
        }

        public MainWindow()
        {

            try
            {
                InitializeComponent();

                // initialize tabItem array
                _tabItems = new List<TabItem>();

                // add a tabItem with + in header 
                TabItem tabAdd = new TabItem();
                tabAdd.Header = addTabHeader;

                _tabItems.Add(tabAdd);

                RMGroup group = new RMGroup();
                group.Name = "Group";
                // add first tab
                this.AddTabItem(group);

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                tabDynamic.SelectedIndex = 0;

                OpenConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private TabItem AddTabItem(RMGroup group)
        {
            int count = _tabItems.Count;

            // create new tab item
            TabItem tab = new TabItem();
            tab.Header = group.Name;
            tab.Name = string.Format("tab{0}", count);
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;

            // add controls to tab item, this case I added just a text box

            RecordSelection recordSelection = new RecordSelection();
            recordSelection.Populate(group.Records);
            tab.Content = recordSelection;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(count - 1, tab);
            return tab;
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


        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
