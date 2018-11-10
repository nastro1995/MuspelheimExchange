using System;
using System.Collections.Generic;
using System.IO;
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
using MuspelScape.Objects;
using Newtonsoft.Json;

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

        public bool IsCachingItemsEnabled { get; set; }//to be added to options as a setting for user.

        public ItemWindow(Basic_ItemInfo item_info, bool useOptions = false)
        {
            InitializeComponent();
            if (useOptions)
            {
                //while online and viewing a item, save its data for offline use, if set to true
                IsCachingItemsEnabled = true;//to be loaded from settings
            }
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
                CacheItem(item);
            }
        }

        public void CacheItem(Item item)
        {
            if (IsCachingItemsEnabled)
            {
                string json = File.ReadAllText(AppFoldersAndFiles.ItemsCachePath);
                OfflineViewedItems data = JsonConvert.DeserializeObject<OfflineViewedItems>(json);
                if (data != null)
                {
                    if (!data.Items.Any(i => i.Id == item.Id))
                    {
                        data.Items.Add(item);
                        string json_new = JsonConvert.SerializeObject(data);
                        AppFoldersAndFiles.FileCreate(AppFoldersAndFiles.ItemsCachePath, json_new);
                    }
                }
                else
                {
                    data = new OfflineViewedItems();
                    if (!data.Items.Any(i => i.Id == item.Id))
                    {
                        data.Items.Add(item);
                        string json_new = JsonConvert.SerializeObject(data);
                        AppFoldersAndFiles.FileCreate(AppFoldersAndFiles.ItemsCachePath, json_new);
                    }
                }
            }
        }

        private void ItemWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IsCachingItemsEnabled = true;
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
