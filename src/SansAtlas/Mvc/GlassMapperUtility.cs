using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace SansAtlas.Mvc
{
    public static class GlassMapperUtility
    {
        public static ISitecoreService GetSitecoreService()
        {
            return ServiceLocator.ServiceProvider.GetService<ISitecoreService>();
        }
    }
}
