using SansAtlas.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SansAtlas.Extensions
{
    public static class LogExtensions
    {

        public static string GetFullTypeName(string callerMemberName, string callerTypeName)
        {
            var seperator = ".";
            if (string.IsNullOrEmpty(callerTypeName)) seperator = "";
            return $"{callerTypeName}{seperator}{callerMemberName}";
        }

        public static void WithExceptionLogging(this ILogService log, Action action, object owner = null, bool sinkException = false)
        {
            using (log.WithLogScope(owner))
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    log.Error("WithExceptionLogging caught exception: ", exception);
                    if (!sinkException) throw;
                }
            }
        }
    }
}
