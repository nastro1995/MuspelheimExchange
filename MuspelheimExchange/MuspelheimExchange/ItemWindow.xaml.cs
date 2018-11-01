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

        public ItemWindow(Basic_ItemInfo item_info)
        {
            InitializeComponent();
            Item_Info = item_info;
            Loaded += ItemWindow_Loaded;
        }

        private void ItemWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Item_Info != null)
            {
                CurrentItem = GE.GetItem(Item_Info.Id);
                if (CurrentItem != null)
                {
                    Title += " - " + CurrentItem.Name;
                    Icon = new BitmapImage(new Uri(CurrentItem.Icon));
                    Item_Grid.DataContext = CurrentItem;
                }
            }
        }
    }
}
