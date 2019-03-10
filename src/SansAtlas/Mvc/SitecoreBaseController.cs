using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
//using Microsoft.Practices.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using SansAtlas.Mapping;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.Mvc
{
    public class SitecoreBaseController : SitecoreController
    {
        private readonly ISitecoreService _sitecoreService;
        private readonly IMvcContext _mvcContext;

        public SitecoreBaseController()
            : this(ServiceLocator.ServiceProvider.GetService<ISitecoreService>(),
                  ServiceLocator.ServiceProvider.GetService<IMvcContext>())
        {
        }

        public SitecoreBaseController(ISitecoreService sitecoreService, IMvcContext mvcContext)
        {
            _sitecoreService = sitecoreService;
            _mvcContext = mvcContext;
            
        }


        ///// <summary>
        ///// Maps model property values from Sitecore item using FieldMapAttribute metadata from the model class
        ///// </summary>
        ///// <typeparam name="T">Model type</typeparam>
        ///// <param name="from">Sitecore item</param>
        ///// <returns>Model</returns>
        ///// <remarks>This API is currrently in beta</remarks>
        //[Obsolete]
        //protected T Map<T>(T to, Item from)
        //    where T : class
        //{
        //    return _itemMapper.Map<T>(from);
        //}

        protected T Map<T>(Item from)
            where T : class
        {
            return _sitecoreService.GetItem<T>(from);
        }

        protected Item SiteRootItem
        {
            get
            {
                var database = Sitecore.Context.Database;
                return database.GetItem(Sitecore.Context.Site.RootPath);
            }
        }

        protected Item SiteStartItem
        {
            get
            {
                var database = Sitecore.Context.Database;
                return database.GetItem(Sitecore.Context.Site.StartPath);
            }
        }

        /// <summary>
        /// Gets the current item from the Sitecore.Context class
        /// </summary>
        protected Item CurrentItem
        {
            get
            {
                return Sitecore.Context.Item;
            }
        }

        /// <summary>
        /// Gets boolean value that indicate whether the current rendering context in page editor mode
        /// </summary>
        /// <returns>True if is in page editor mode, otherwise false</returns>
        public bool IsPageEditor()
        {
            return Sitecore.Context.PageMode.IsExperienceEditor;
        }

    }
}
