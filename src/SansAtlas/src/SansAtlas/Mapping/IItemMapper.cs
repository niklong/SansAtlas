using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.Mapping
{
    public interface IItemMapper
    {
        TModel Map<TModel>(TModel model, Item item);

        TModel Map<TModel>(Item item) where TModel : class;
    }
}
