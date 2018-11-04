using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MuspelScape;
using MuspelScape.Models;
using MuspelScape.Models.Catalogue;
using MuspelScape.Models.Graphs;
using MuspelScape.Models.Items;

namespace MuspelheimExchange.Views
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private List<Basic_ItemInfo> ItemInfos { get; set; }
        private MainWindow RootWindow { get; set; }

        public SearchPage(MainWindow rootWindow)
        {
            InitializeComponent();
            //List<Basic_ItemInfo> itemsBasicInfo = GE.GetBasicItemsInfo();
            //List<Basic_ItemInfo> bronzeItems = new List<Basic_ItemInfo>(
            //    itemsBasicInfo.Where(i => i.Name.StartsWith("Bronze"))
            //);
            //Item bDagger = GE.GetItem(bronzeItems.Single(
            //    i => i.Name.ToLower() == "bronze dagger").Id
            //);
            //CatalogueView cat1 = GE.GetCatalogue(1, 'b', 1);
            //GraphView graph1 = GE.GetGraph(bDagger.Id);
            RootWindow = rootWindow;
            Loaded += SearchPage_Loaded;
        }

        private void SearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            ItemInfos = GE.GetBasicItemsInfo();
            ResetDisplay(0);
            RootWindow.ToggleBarState(false);
            Search_Mode.ItemsSource = new List<string>()
            {
                "Contains",
                "StartsWith",
                "Exact"
            };
            Search_Mode.SelectedIndex = 0;
        }

        private void ResetDisplay(int sortMode = 0)
        {
            if (sortMode >= 0 && sortMode <= 2)
            {
                if (sortMode == 0)
                {
                    Display.ItemsSource = ItemInfos;
                }
                else if (sortMode == 1)
                {
                    Display.ItemsSource = ItemInfos.OrderBy(i => i.Name);
                }
                else if (sortMode == 2)
                {
                    Display.ItemsSource = ItemInfos.OrderByDescending(i => i.Name);
                }
            }
        }

        private void Search_Item(string input, int sortMode = 0)
        {
            List<Basic_ItemInfo> searchResults = null;
            if (Search_Mode.SelectedItem is string searchMode)
            {
                if (searchMode == "Contains")
                {
                    searchResults = new List<Basic_ItemInfo>(ItemInfos.Where(i => i.Name.ToLower().Contains(input.ToLower())));
                }
                else if (searchMode == "StartsWith")
                {
                    searchResults = new List<Basic_ItemInfo>(ItemInfos.Where(i => i.Name.ToLower().StartsWith(input.ToLower())));
                }
                else if (searchMode == "Exact")
                {
                    searchResults = new List<Basic_ItemInfo>(ItemInfos.Where(i => i.Name.ToLower() == input.ToLower()));
                }
                if (sortMode >= 0 && sortMode <= 2)
                {
                    if (sortMode == 0)
                    {
                        Display.ItemsSource = searchResults;
                    }
                    else if (sortMode == 1)
                    {
                        Display.ItemsSource = searchResults.OrderBy(i => i.Name);
                    }
                    else if (sortMode == 2)
                    {
                        Display.ItemsSource = searchResults.OrderByDescending(i => i.Name);
                    }
                }
            }
        }

        private void Search_Using_Input(int sortMode = 0)
        {
            if (Search_Input.Text is string input && input != "")
            {
                Search_Item(input, sortMode);
            }
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Search_Input.Text is string input)
            {
                if (input != "")
                {
                    Search_Item(input);
                }
                else
                {
                    ResetDisplay();
                }
            }
        }

        private void Search_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_Input.Text is string input
                && input == "")
            {
                ResetDisplay();
            }
        }

        private void Display_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Display.SelectedItem is Basic_ItemInfo item_info)
            {
                ItemWindow iw = new ItemWindow(item_info)
                {
                    Owner = RootWindow
                };
                iw.Show();
            }
        }

        private void Sort_None_Btn_Click(object sender, RoutedEventArgs e)
        {
            ResetDisplay();
            Search_Using_Input();
        }

        private void Sort_Name_Asc_Btn_Click(object sender, RoutedEventArgs e)
        {
            ResetDisplay(1);
            Search_Using_Input(1);
        }

        private void Sort_Name_Desc_Btn_Click(object sender, RoutedEventArgs e)
        {
            ResetDisplay(2);
            Search_Using_Input(2);
        }
    }
}
