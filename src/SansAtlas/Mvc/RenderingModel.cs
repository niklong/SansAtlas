using SansAtlas.Logging;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Mvc.Presentation;
using System;

namespace SansAtlas.Mvc
{
    public abstract class RenderingModel<TViewModel> : RenderingModel
        where TViewModel : class
    {
        private readonly ISitecoreService _sitecoreService;
        private readonly IMvcContext _mvcContext;

        public RenderingModel()
            : this(ServiceLocator.ServiceProvider.GetService<ISitecoreService>(),
                  ServiceLocator.ServiceProvider.GetService<IMvcContext>())
        {
        }

        public RenderingModel(ISitecoreService sitecoreService, IMvcContext mvcContext)
        {
            _sitecoreService = sitecoreService;
            _mvcContext = mvcContext;
        }

        public ISitecoreService SitecoreService { get => _sitecoreService; }
        public IMvcContext MvcContext { get => _mvcContext; }

        public TViewModel ViewModel { get; private set; }

        public bool HasError { get; set; }

        public virtual string ErrorMessage { get; set; }

        public string RenderingId
        {
            get
            {
                return string.Format("{0}{1}",
                    this.Rendering.RenderingItem.Name.Replace(" ", "_"),
                    this.Rendering.UniqueId.ToString().Substring(0, 6));
            }
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

        public bool HasValueOrIsPageEditor(string fieldRenderingString)
        {
            return IsPageEditor() || HasValue(fieldRenderingString);
        }

        public bool HasValue(string fieldRenderingString)
        {
            return (!string.IsNullOrEmpty(fieldRenderingString?.ToString()));
        }

        //public bool HasValueOrIsPageEditor(Glass.Mapper.Sc.Fields.Image fieldRenderingString)
        //{
        //    return IsPageEditor() || HasValue(fieldRenderingString);
        //}

        //public bool HasValue(Glass.Mapper.Sc.Fields.Image fieldRenderingString)
        //{
        //    return (fieldRenderingString?.MediaItem != null);
        //}

        /// <summary>
        /// Initialises the rendering model
        /// </summary>
        /// <param name="rendering"></param>
        public override void Initialize(Rendering rendering)
        {
            try
            {
                var dataSourceParser = new DataSourceParser();
                var dataSource = dataSourceParser.Parse(rendering.DataSource);
                ViewModel = InitialiseViewModel(rendering, dataSource);
                base.Initialize(rendering);
            }
            catch (Exception ex)
            {
                var logService = default(ILogService);

                try
                {
                    logService = ServiceLocator.ServiceProvider.GetService<ILogService>();
                }
                catch (Exception) { } // the ILogService interface has not been configured

                if (logService != null)
                {
                    logService.Error(ex.ToString(), this);
                }

                this.HasError = true;
                this.ErrorMessage = ex.Message;

                // do not swallow exceptions - very annoying Deloitte!!!
                throw;
            }
        }

        /// <summary>
        /// Initialises an instance of the TViewModel type
        /// </summary>
        /// <param name="rendering">Rendering context</param>
        /// <param name="dataSource">Data source</param>
        /// <returns>View model object</returns>
        protected abstract TViewModel InitialiseViewModel(Rendering rendering, DataSource dataSource);

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
        //    throw new NotImplementedException();
        //}

        protected T Map<T>(Item from)
            where T : class
        {
            return _sitecoreService.GetItem<T>(from);
        }
    }
}