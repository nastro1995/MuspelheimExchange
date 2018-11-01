using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Models.Items
{
    public class Item
    {
        public string Icon { get; set; }
        public string Icon_large { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string TypeIcon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Current Current { get; set; }
        public Today Today { get; set; }
        public string Members { get; set; }
        public Day30 Day30 { get; set; }
        public Day90 Day90 { get; set; }
        public Day180 Day180 { get; set; }
    }
}
