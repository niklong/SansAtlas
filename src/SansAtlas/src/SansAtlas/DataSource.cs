using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SansAtlas
{
    public class DataSource
    {
        public Item Item { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public bool HasItems
        {
            get { return Item != null || Items.Count() > 0; }
        }

        public static DataSource Empty
        {
            get
            {
                return new DataSource
                {
                    Item = null,
                    Items = Enumerable.Empty<Item>()
                };
            }
        }
    }
}