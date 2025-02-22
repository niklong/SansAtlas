﻿using System.Collections.Generic;
//using Microsoft.Practices.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetRenderingDatasource;

namespace SansAtlas.Multisite.DataSource
{
    public class GetDatasourceLocationFromProviders
    {
        private const string DatasourceLocationFieldName = "Datasource Location";
        private readonly IEnumerable<IDatasourceProvider> _providers;

        public GetDatasourceLocationFromProviders()
        {
            // TODO - get all instances doesn't appear to work... 
            _providers = new List<IDatasourceProvider> { ServiceLocator.ServiceProvider.GetService<IDatasourceProvider>() };
        }

        public void Process(GetRenderingDatasourceArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.IsNotNull(_providers, typeof(IDatasourceProvider));

            var source = args.RenderingItem[DatasourceLocationFieldName];

            foreach (var datasourceProvider in _providers)
            {
                if (!datasourceProvider.CanAct(source))
                    continue; // current provider cannot act on this data source

                var contextItem = args.ContentDatabase.GetItem(args.ContextItemPath);
                                
                var datasources = datasourceProvider.GetDatasources(source, contextItem);
                args.DatasourceRoots.AddRange(datasources);
            }
        }

    }
}
