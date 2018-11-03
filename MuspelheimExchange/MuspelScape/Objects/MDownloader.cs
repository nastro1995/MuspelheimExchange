using System;
using System.Collections.Generic;
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
            //todo
        }
    }
}
