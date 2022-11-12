using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace MicroFrontend.Api.Common.Middlewares;

public static class ExceptionHandlerMiddleware
{
    public static void HandleExceptions(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature != null)
                    await context.Response.WriteAsync(contextFeature.Error.Message);
            });
        });
    }
}