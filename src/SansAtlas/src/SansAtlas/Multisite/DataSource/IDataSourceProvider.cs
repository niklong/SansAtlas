using System.Collections.Generic;
using Sitecore.Data.Items;

namespace SansAtlas.Multisite.DataSource
{
    public interface IDatasourceProvider
    {
        IEnumerable<Item> GetDatasources(string name, Item contextItem);

        bool CanAct(string datasourceLocationValue);
    }
}
