using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Objects
{
    public static class AppFoldersAndFiles
    {
        #region Folder Paths
        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;//bin
        public static string AppDataPath = Path.Combine(AppPath, "Data");//bin/Data
        public static string DataOfflinePath = Path.Combine(AppDataPath, "Offline-Data");//bin/Data/Offline-Data
        public static string OfflineCachePath = Path.Combine(AppDataPath, "Cache");//bin/Data/Cache
        #endregion
        #region Item Paths
        public static string ItemsJsonPath = Path.Combine(DataOfflinePath, "items-basic.json");//../Offline-Data/items-basic.json
        public static string ItemsCachePath = Path.Combine(OfflineCachePath, "viewed-items.json");//../Cache/viewed-items.json
        #endregion

        public static void CreateFolders()
        {
            /* Creates:
             * bin/Data/
             * bin/Data/Offline-Data/
             * bin/Data/Cache/
             */
            FolderCreate(AppDataPath);
            FolderCreate(DataOfflinePath);
            FolderCreate(OfflineCachePath);
        }

        public static void FolderCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void FileCreate(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        #region Item Methods
        public static void ItemJsonCreate(string json_data)
        {
            FileCreate(ItemsJsonPath, json_data);
        }

        public static OfflineBasicItemsData ReadOfflineItemsJson()
        {
            string json = File.ReadAllText(ItemsJsonPath);
            return JsonConvert.DeserializeObject<OfflineBasicItemsData>(json);
        }

        public static void ItemsCacheAppend(string json)
        {
            FileCreate(ItemsCachePath, json);
        }

        public static OfflineViewedItems ReadItemsCacheFile()
        {
            string json = File.ReadAllText(ItemsCachePath);
            return JsonConvert.DeserializeObject<OfflineViewedItems>(json);
        }

        public static void ClearItemsCache()
        {
            FileCreate(ItemsCachePath, "");
        }
        #endregion
    }
}
