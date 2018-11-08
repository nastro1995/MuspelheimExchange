using MuspelScape.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Objects
{
    public class OfflineViewedItems
    {
        public List<Item> Items { get; set; }

        public OfflineViewedItems()
        {
            Items = new List<Item>();
        }
    }
}
