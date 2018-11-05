using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MuspelScape.Models;
using MuspelScape.Models.Items;
using MuspelScape.Objects;
using MuspelScape.Models.Catalogue;
using MuspelScape.Models.Graphs;
using System.Windows;

namespace MuspelScape
{
    public class GE
    {
        private const string OSRS_ItemDb_LastUpdatedUrl = "https://secure.runescape.com/m=itemdb_oldschool/api/info.json";
        private const string BaseUrl = "http://services.runescape.com/m=itemdb_oldschool";
        private const string BasicItemsIdUrl = "https://pastebin.com/raw/LhxJ7GRG/Objects_87.json";
        private const string ItemUrl = "/api/catalogue/detail.json?item=";
        private static string CatalogueUrl(int category, char startChar, int page) 
            => $"{BaseUrl}/api/catalogue/items.json?category={category}&alpha={startChar}&page={page}";
        private static string GraphUrl(int itemId)
            => $"{BaseUrl}/api/graph/{itemId}.json";

        public static Item GetItem(int itemId)
        {
            string url = $"{BaseUrl}{ItemUrl}{itemId}";
            Raw_Item result_raw = null;
            Item result = null;
            string json = MDownloader.GetJSON(url);
            if (json != null)
            {
                if (json.Length > 0)
                {
                    result_raw = JsonConvert.DeserializeObject<Raw_Item>(json);
                    if (result_raw != null)
                    {
                        result = result_raw.Item;
                    }
                }
            }
            else {/*load offline data if any, else error*/ }
            return result;
        }

        public static List<Basic_ItemInfo> GetBasicItemsInfo()
        {
            List<Basic_ItemInfo> result = null;
            try
            {
                string json = MDownloader.GetJSON(BasicItemsIdUrl);
                result = JsonConvert.DeserializeObject<List<Basic_ItemInfo>>(json);
            }
            catch (WebException)
            {
                MessageBoxResult mbResult = MessageBox.Show(
                    "Error retrieving items, press 'Yes' to retry,\n 'No' will try to use offline data if any.", 
                    "Error!", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes
                );
                if (mbResult == MessageBoxResult.Yes)
                {
                    result = GetBasicItemsInfo();
                }
                else if (mbResult == MessageBoxResult.No)
                {
                    OfflineBasicItemsData offlineItems = AppFoldersAndFiles.ReadOfflineItemsJson();
                    result = offlineItems.Items;
                }
            }
            catch (ArgumentNullException)
            {
                OfflineBasicItemsData offlineItems = AppFoldersAndFiles.ReadOfflineItemsJson();
                result = offlineItems.Items;
            }
            return result;
        }

        //long load, allot of data!
        //public static List<Item> GetItems()
        //{
        //    List<Item> result = new List<Item>();
        //    List<Item_Base> result_raw = GetBasicItemsInfo();
        //    foreach (Item_Base item_raw in result_raw)
        //    {
        //        result.Add(GetItem(item_raw.Id));
        //    }
        //    return result;
        //}

        public static CatalogueView GetCatalogue(ItemCategories category, char startChar, int page)
        {
            string url = CatalogueUrl((int)category, startChar, page);
            CatalogueView result = null;
            string json = MDownloader.GetJSON(url);
            if (json != null)
            {
                if (json.Length > 0)
                {
                    result = JsonConvert.DeserializeObject<CatalogueView>(json);
                }
            }
            return result;
        }

        public static List<ItemCategories> GetItemCategories()
        {
            List<ItemCategories> result = new List<ItemCategories>();
            string[] categories_raw = Enum.GetNames(typeof(ItemCategories));
            foreach (string _category in categories_raw)
            {
                result.Add((ItemCategories)Enum.Parse(typeof(ItemCategories), _category));
            }
            return result;
        }

        //GraphView object needs work
        //public static GraphView GetGraph(int itemId)
        //{
        //    string url = GraphUrl(itemId);
        //    GraphView result = null;
        //    string json = MDownloader.GetJSON(url);
        //    if (json.Length > 0)
        //    {
        //        result = JsonConvert.DeserializeObject<GraphView>(json);
        //    }
        //    return result;
        //}

        public static RuneDate GetDbLastUpdated()
        {
            RuneDate result = null;
            string json = MDownloader.GetJSON(OSRS_ItemDb_LastUpdatedUrl);
            if (json != null)
            {
                if (json.Length > 0)
                {
                    result = JsonConvert.DeserializeObject<RuneDate>(json);
                }
            }
            return result;
        }
    }
}
