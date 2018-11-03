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
            ResetDisplay(1);
            RootWindow.ToggleBarState(false);
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

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Search_Input.Text is string input)
            {
                if (input != "")
                {
                    List<Basic_ItemInfo> searchResults = new List<Basic_ItemInfo>(ItemInfos.Where(i => i.Name.ToLower().Contains(input)));
                    Display.ItemsSource = searchResults;
                }
                else
                {
                    ResetDisplay(1);
                }
            }
        }

        private void Search_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_Input.Text is string input
                && input == "")
            {
                ResetDisplay(1);
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
    }
}
