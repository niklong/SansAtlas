using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.ErrorHandling
{
    public interface ICustomErrorView
    {
        string ErrorViewFile { get; }
    }
}
