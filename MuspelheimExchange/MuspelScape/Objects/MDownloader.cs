using MuspelScape.Models;
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
            RuneDate latestRuneDate = GE.GetDbLastUpdated();
            double data_new_LastUpdated = latestRuneDate.LastConfigUpdateRuneday;
            bool isDataOld = true;
            if (File.Exists(AppFoldersAndFiles.ItemsJsonPath))
            {
                string json_old = File.ReadAllText(AppFoldersAndFiles.ItemsJsonPath);
                if (json_old != "")
                {
                    OfflineBasicItemsData data_old = JsonConvert.DeserializeObject<OfflineBasicItemsData>(json_old);
                    isDataOld = (data_new_LastUpdated > data_old.LastUpdated);
                }
            }
            if (isDataOld)
            {
                OfflineBasicItemsData data_new = new OfflineBasicItemsData(data_new_LastUpdated, GE.GetBasicItemsInfo());
                string json_new = JsonConvert.SerializeObject(data_new);
                AppFoldersAndFiles.ItemJsonCreate(json_new);
            }
        }
    }
}
