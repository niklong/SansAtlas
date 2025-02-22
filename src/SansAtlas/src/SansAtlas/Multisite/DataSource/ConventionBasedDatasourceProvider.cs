﻿using Sitecore.Data.Items;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SansAtlas.Multisite.DataSource
{
    public class ConventionBasedDatasourceProvider : IDatasourceProvider
    {
        public const string ContentModuleFolder = "Content Modules";
        public const string ConventionDatasourcePrefix = "convention:";
        public const string ConventionDatasourceMatchPattern = @"^" + ConventionDatasourcePrefix + @"(.*)$";

        public IEnumerable<Item> GetDatasources(string source, Item contextItem)
        {
            // get the site info for the current context item
            var siteInfo = SiteContextFactory.Sites.Where(
                s => s.RootPath != "" && contextItem.Paths.Path.ToLower().StartsWith(s.RootPath.ToLower()))
                .OrderByDescending(s => s.RootPath.Length)
                .FirstOrDefault();

            // no site info found (e.g. __standard values item), show the entire tree. 
            if (siteInfo == null) return new List<Item> { contextItem.Database.SitecoreItem };

            var path = $"{siteInfo?.RootPath}/{ContentModuleFolder}/{GetModuleName(source)}";

            return new List<Item> { contextItem.Database.GetItem(path) };
        }

        public bool CanAct(string datasourceLocationValue)
        {
            var match = Regex.Match(datasourceLocationValue, ConventionDatasourceMatchPattern);
            return match.Success;
        }

        private static string GetModuleName(string datasourceLocationValue)
        {
            var match = Regex.Match(datasourceLocationValue, ConventionDatasourceMatchPattern);
            return !match.Success ? null : match.Groups[1].Value;
        }

    }
}
