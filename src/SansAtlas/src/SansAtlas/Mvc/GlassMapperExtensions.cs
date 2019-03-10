using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Web;
using System.Web.Mvc;

namespace SansAtlas.Mvc
{
    public static class GlassMapperExtensions
    {
        #region [ MvcContext extensions ]

        public static T GetDataSourceOrPageContextItem<T>(this IMvcContext context) where T : class
        {
            return context.HasDataSource ? context.GetDataSourceItem<T>() : context.GetPageContextItem<T>();
        }

        #endregion

        #region [ SitecoreService extensions ]

        public static IMvcContext MvcContext(this ISitecoreService service)
        {
            return new MvcContext(service);
        }

        public static Item GetStartItem(this ISitecoreService service)
        {
            return Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
        }

        public static Item GetContextItem(this ISitecoreService service)
        {
            return Sitecore.Context.Item;
        }

        public static Item GetItem(this ISitecoreService service, ID id)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (ID.IsNullOrEmpty(id))
                return null;

            return Sitecore.Context.Database.GetItem(id);
        }

        public static T GetItem<T>(this ISitecoreService service, ID id) where T: class
        {
            if(service == null)
                throw new ArgumentNullException(nameof(service));

            if (ID.IsNullOrEmpty(id))
                return null;

            return service.GetItem<T>(id);
        }

        #endregion

        #region [ IGlassHtml extensions ]

        public static IMvcContext MvcContext(this IGlassHtml glassHtml)
        {
            return MvcContext(glassHtml.SitecoreService);
        }

        public static bool HasDataSource(this IGlassHtml glassHtml)
        {
            return MvcContext(glassHtml).DataSourceItem != null;
        }

        public static T DataSourceItem<T>(this IGlassHtml glassHtml) where T: class
        {
            return MvcContext(glassHtml).GetDataSourceItem<T>();
        }

        #endregion

        #region [ HtmlHelper extensions ]

        public static IHtmlString Raw(this HtmlHelper helper, string value, bool expandLinks)
        {
            return new HtmlString(!string.IsNullOrEmpty(value) ? (expandLinks ? DynamicLink.ExpandLinks(value) : value) : value);
        }

        #endregion
    }
}
