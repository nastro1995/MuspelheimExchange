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
using System.Windows.Shapes;
using MuspelScape;
using MuspelScape.Models.Items;

namespace MuspelheimExchange
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        private Item CurrentItem { get; set; }
        private Basic_ItemInfo Item_Info { get; set; }
        private Item Item { get; set; }

        public ItemWindow(Basic_ItemInfo item_info)
        {
            InitializeComponent();
            Item_Info = item_info;
            Item = null;
            Loaded += ItemWindow_Loaded;
        }

        public ItemWindow(Item item)
        {
            InitializeComponent();
            Item_Info = null;
            Item = item;
            Loaded += ItemWindow_Loaded;
        }

        private void LoadItem(Item item)
        {
            if (item != null)
            {
                Title += " - " + item.Name;
                Icon = new BitmapImage(new Uri(item.Icon));
                Item_Grid.DataContext = item;
            }
        }

        private void ItemWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Item_Info != null)
            {
                CurrentItem = GE.GetItem(Item_Info.Id);
                LoadItem(CurrentItem);
            }
            else if (Item != null)
            {
                CurrentItem = Item;
                LoadItem(CurrentItem);
            }
        }
    }
}
