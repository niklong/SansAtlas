using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SansAtlas.ErrorHandling
{
    public class ErrorRenderingModel
    {
        public string RenderingName { get; private set; }

        public Exception Exception { get; private set; }

        public ErrorRenderingModel(string renderingName, Exception exception)
        {
            RenderingName = renderingName;
            Exception = exception;
        }
    }
}