using Registry_Manager.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Windows.Input;
using Registry_Manager.UI.Windows.MessageBox;

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

        public static RMConfig rmConfig;
        private static string _loadedFilePath = string.Empty;

        public MainWindow()
        {

            try
            {
                InitializeComponent();

                this.DataContext = this;

                InitializeDefaultConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeDefaultConfig()
        {
            RMGroup group = new RMGroup();
            group.Name = "Group";
            
            // add first tab
            var config = new RMConfig();
            config.Name = "My Config";
            config.Groups.Add(group);

            ImportConfig(config);
        }

        private void InitializeAddTab()
        {
            // add a tabItem with + in header 
            TabItem tabAdd = new TabItem();
            tabAdd.Header = addTabHeader;
            var bc = new BrushConverter();
            tabAdd.Foreground = (Brush)bc.ConvertFrom("#F1F1F1");
            tabAdd.Width = 40;
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
                tab.Name = string.Format("Group{0}", count);
                tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;

                RecordSelection recordSelection = new RecordSelection();
                recordSelection.Populate(group);
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
                    group.Name = "Group " + rmConfig.Groups.Where(x=> x.Name.StartsWith("Group")).Count();
                    rmConfig.Groups.Add(group);

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

        private void tabDynamicRename(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item)
            {
                RenameGroupTab modalWindow = new RenameGroupTab();
                modalWindow.Owner = Application.Current.MainWindow;
                modalWindow.Left = modalWindow.Owner.Left + modalWindow.Owner.Width / 3;
                modalWindow.Top = modalWindow.Owner.Top + modalWindow.Owner.Height / 2;
                var contextMenu = item.Parent as ContextMenu;
                var tebItem = contextMenu.PlacementTarget as TextBlock;
                RenameGroupTab.groupName = tebItem.Text;
                modalWindow.ShowDialog();

                string valueFromModalTextBox = RenameGroupTab.groupName;

                var dContext = item.DataContext;
                var groupTitle = dContext.ToString();
                var rmGroup = rmConfig.Groups.SingleOrDefault(x => x.Name == groupTitle);
                if (rmGroup != null && !string.IsNullOrEmpty(valueFromModalTextBox))
                {
                    rmGroup.Name = valueFromModalTextBox;
                    _tabItems.SingleOrDefault(x => x.Header.ToString() == groupTitle).Header = valueFromModalTextBox;

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

            rmConfig = config;

            if(!string.IsNullOrEmpty(config.Name))
            {
                Title = "Registry Manager - " + config.Name;
            }
            
        }

      

        #region Actions
        private void OpenAction()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                _loadedFilePath = openFileDialog.FileName;
                var jsonConfig = File.ReadAllText(openFileDialog.FileName);
                var config = JsonConvert.DeserializeObject<RMConfig>(jsonConfig);

                ImportConfig(config);
            }
        }

        private static void SaveAsAction()
        {
            var jsonConfig = JsonConvert.SerializeObject(rmConfig, Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                _loadedFilePath = saveFileDialog.FileName;
                File.WriteAllText(saveFileDialog.FileName, jsonConfig);
            }
        }
        #endregion


        #region Events
        private void NewConfig_Click(object sender, RoutedEventArgs e)
        {
            InitializeDefaultConfig();
        }
        private void OpenConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenAction();
        }

        private void SaveAsConfig_Click(object sender, RoutedEventArgs e)
        {
            SaveAsAction();
        }

        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (string.IsNullOrEmpty(_loadedFilePath))
                {
                    SaveAsAction();
                    return;
                }
                var jsonConfig = JsonConvert.SerializeObject(rmConfig, Formatting.Indented);
                File.WriteAllText(_loadedFilePath, jsonConfig);

                DarkMesssageBox darkMesssageBox = new DarkMesssageBox();
                darkMesssageBox.Text = "Configuration is saved";
                darkMesssageBox.Owner = Application.Current.MainWindow;
                darkMesssageBox.Left = darkMesssageBox.Owner.Left + darkMesssageBox.Owner.Width / 3;
                darkMesssageBox.Top = darkMesssageBox.Owner.Top + darkMesssageBox.Owner.Height / 3;
                darkMesssageBox.ShowDialog();
            }

            if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                OpenAction();
                return;
            }
            if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                InitializeDefaultConfig();
                return;
            }
        } 
        #endregion
    }
}
