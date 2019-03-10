using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
//using Microsoft.Practices.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Data.Items;

namespace SansAtlas.Mapping
{
    public class ItemMapper : IItemMapper
    {
        ISitecoreService _sitecoreService;

        public ItemMapper()
            : this(ServiceLocator.ServiceProvider.GetService<ISitecoreService>())
        {
        }

        public ItemMapper(ISitecoreService sitecoreService)
        {
            _sitecoreService = sitecoreService;
        }

        [Obsolete]
        public TModel Map<TModel>(TModel model, Item item)
        {
            throw new NotImplementedException();
        }

        public TModel Map<TModel>(Item item) where TModel : class
        {
            return _sitecoreService.GetItem<TModel>(item);
        }
    }
}
