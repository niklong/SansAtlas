﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas
{
    internal static class Constants
    {
        public static class CacheKeys
        {
            public const string MappingMetaData = "DD.Atlas.Sitecore-MappingMetaDataDictionary";
        }

        public static class Database
        {
            public const string CoreDatabaseName = "core";
            public const string MasterDatabaseName = "master";
            public const string WebDatabaseName = "web";
        }

        public static class Index
        {
            public const string WebIndexName = "sitecore_web_index";
            public const string MasterIndexName = "sitecore_master_index";
        }
    }
}
