using MuspelScape.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuspelScape.Models.Catalogue
{
    public class CatalogueView
    {
        public int Total { get; set; }
        public List<Item> Items { get; set; }
    }
}
