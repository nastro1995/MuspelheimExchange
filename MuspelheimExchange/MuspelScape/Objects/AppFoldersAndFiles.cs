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
        public static string AppDataPath = Path.Combine(AppPath, "Data");//~/Data
        public static string DataOfflinePath = Path.Combine(AppDataPath, "Offline-Data");//~/Data/Offline-Data
        public static string OfflineCachePath = Path.Combine(AppDataPath, "Cache");//~/Data/Cache
        #endregion
        #region Item Paths
        public static string ItemsJsonPath = Path.Combine(DataOfflinePath, "items-basic.json");//~/Data/Offline-Data/items-basic.json
        public static string ItemsCachePath = Path.Combine(OfflineCachePath, "viewed-items.json");//~/Data/Cache/viewed-items.json
        public static string OptionsJsonPath = Path.Combine(AppDataPath, "options.json");//~/Data/options.json
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
        public static void OptionsFileCreate(string json)
        {
            FileCreate(OptionsJsonPath, json);
        }

        public static string ReadOptionsFile()
        {
            return File.ReadAllText(OptionsJsonPath);
        }

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
