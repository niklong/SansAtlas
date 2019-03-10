using System.Web;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;

namespace SansAtlas.DynamicPlaceholders
{
    public static class SitecoreHelperExtension
    {
        public static HtmlString DynamicPlaceholder(this SitecoreHelper helper, string dynamicKey)
        {
            var currentRenderingId = RenderingContext.Current.Rendering.UniqueId;
            return helper.Placeholder($"{dynamicKey}_dph_{currentRenderingId}");
        }
    }
}
