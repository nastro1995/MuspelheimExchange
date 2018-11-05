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
        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string AppDataPath = Path.Combine(AppPath, "Data");
        public static string DataOfflinePath = Path.Combine(AppDataPath, "Offline-Data");
        public static string ItemsJsonPath = Path.Combine(DataOfflinePath, "items-basic.json");

        public static void CreateFolders()
        {
            FolderCreate(AppDataPath);
            FolderCreate(DataOfflinePath);
        }

        public static void ItemJsonCreate(string json_data)
        {
            FileCreate(ItemsJsonPath, json_data);
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
    }
}
