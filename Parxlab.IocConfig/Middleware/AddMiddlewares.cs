using Microsoft.AspNetCore.Builder;
using Parxlab.IocConfig.Extensions;

namespace Parxlab.IocConfig.Middleware
{
    public static class AddMiddlewareExtentions
    {
        public static void AddCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMainMiddlewares();
            //var rewriteOptions = new RewriteOptions();
            //rewriteOptions.Rules.Add(new NonWwwRewriteRule());
            //app.UseRewriter(rewriteOptions);

        }
    }
}
