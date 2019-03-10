using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.Extensions
{
    [Obsolete]
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
