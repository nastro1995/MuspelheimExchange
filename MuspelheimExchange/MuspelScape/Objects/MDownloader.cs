using MuspelScape.Models.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Objects
{
    public class MDownloader
    {
        public static string GetJSON(string url)
        {
            string result = null;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    result = wc.DownloadString(url);
                }
            }
            catch (WebException)
            {
                result = null;
            }
            return result;
        }

        public static void UpdateItemsOfflineJSON()
        {
            double data_new_Updated = 20.20;//GE.GetCurrentRuneDate
            bool isNewDataOld = false;
            if (File.Exists(AppFoldersAndFiles.ItemsJsonPath))
            {
                string json_old = File.ReadAllText(AppFoldersAndFiles.ItemsJsonPath);
                if (json_old != "")
                {
                    OfflineBasicItemsData data_old = JsonConvert.DeserializeObject<OfflineBasicItemsData>(json_old);
                    isNewDataOld = data_new_Updated > data_old.LastUpdated;
                }
            }
            if (isNewDataOld)
            {
                OfflineBasicItemsData data_new = new OfflineBasicItemsData(data_new_Updated, GE.GetBasicItemsInfo());
                string json_new = JsonConvert.SerializeObject(data_new);
                AppFoldersAndFiles.ItemJsonCreate(json_new);
            }
        }
    }
}
