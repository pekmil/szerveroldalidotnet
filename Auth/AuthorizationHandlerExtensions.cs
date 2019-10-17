using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventApp.Auth
{
    public static class AuthorizationHandlerExtensions
    {
        public static bool TryGetParamValue<T>(this AuthorizationHandlerContext context, string paramName, out T paramValue)
        {
            if (context.Resource is AuthorizationFilterContext mvcContext)
            {
                var routeData = mvcContext.RouteData.Values;
                if (routeData.TryGetValue(paramName, out var paramObj))
                {
                    try
                    {
                        if (paramObj is string paramString && typeof(T) == typeof(Guid))
                        {
                            paramValue = (T)Convert.ChangeType(Guid.Parse(paramString), typeof(T));
                        }
                        else
                        {
                            paramValue = (T)Convert.ChangeType(paramObj, typeof(T));
                        }
                        return true;
                    }
                    catch (InvalidCastException ice)
                    {
                        //ignore
                    }
                }
            }
            paramValue = default(T);
            return false;
        }
    }
}
