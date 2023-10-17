using FactoryAPI.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace FactoryAPI.MiddlewareExtentions
{
    public static class Middleware
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    // Currently used when the ID parameter cannot be found
                    if (contextFeature.Error.GetType() == typeof(NotFoundCustomException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = (int)HttpStatusCode.NotFound,
                            Message = contextFeature.Error.Message,
                        }.ToString());

                    }
                    // Currently Used when the required Name parameter is empty
                    else if (contextFeature.Error.GetType() == typeof(NullCustomException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = (int)HttpStatusCode.Forbidden,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
