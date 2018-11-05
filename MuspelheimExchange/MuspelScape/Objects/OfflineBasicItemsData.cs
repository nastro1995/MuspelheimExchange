using MuspelScape.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Objects
{
    public class OfflineBasicItemsData
    {
        public double LastUpdated { get; set; }
        public List<Basic_ItemInfo> Items { get; set; }

        public OfflineBasicItemsData(double lastUpdated, List<Basic_ItemInfo> items)
        {
            LastUpdated = lastUpdated;
            Items = items;
        }
    }
}
