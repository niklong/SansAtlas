using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.Extensions
{
    public static class SiteContextExtensions
    {
        //public static Item GetStartItem(this SiteContext siteContext)
        //{
        //    var database = Context.Database;
        //    return database.GetItem(siteContext.StartPath);
        //}

        /// <summary>
        /// Note: this works for the master and web indexes only
        /// </summary>
        public static string GetIndexName(this SiteContext site)
        {
            return site.Database.Name.Equals(Constants.Database.MasterDatabaseName, StringComparison.OrdinalIgnoreCase)
                ? Constants.Index.MasterIndexName
                : Constants.Index.WebIndexName;
        }
    }
}
