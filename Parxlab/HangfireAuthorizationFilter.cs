﻿using Hangfire.Dashboard;

namespace Parxlab
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();
            //if (httpContext.User.Identity == null) return false;
            //return httpContext.User.Identity.IsAuthenticated;
            return true;

        }
    }
}
