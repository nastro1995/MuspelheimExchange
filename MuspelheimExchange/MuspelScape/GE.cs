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
            if (json.Length > 0)
            {
                result_raw = JsonConvert.DeserializeObject<Raw_Item>(json);
                if (result_raw != null)
                {
                    result = result_raw.Item;
                }
            }
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
                string offline_json = System.IO.File.ReadAllText(@"F:\Projects\C#\Muspelheim_Dev-OSRS\MuspelheimExchange\MuspelheimExchange\Data\osrs-offline-items.json");
                result = JsonConvert.DeserializeObject<List<Basic_ItemInfo>>(offline_json);
		        MessageBox.Show("You are offline!, you wont be able to view the items.", "Error!");
            }
            return result;
        }

        //hidden, long load on slow internet, allot of data!
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

        public static CatalogueView GetCatalogue(int category, char startChar, int page)
        {
            string url = CatalogueUrl(category, startChar, page);
            CatalogueView result = null;
            string json = MDownloader.GetJSON(url);
            if (json.Length > 0)
            {
                result = JsonConvert.DeserializeObject<CatalogueView>(json);
            }
            return result;
        }

        public static GraphView GetGraph(int itemId)
        {
            string url = GraphUrl(itemId);
            GraphView result = null;
            string json = MDownloader.GetJSON(url);
            if (json.Length > 0)
            {
                result = JsonConvert.DeserializeObject<GraphView>(json);
            }
            return result;
        }
    }
}
