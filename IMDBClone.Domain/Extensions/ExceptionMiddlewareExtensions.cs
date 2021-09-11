using System;
using System.Net;
using IMDBClone.Domain.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace IMDBClone.Domain.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //TODO - Add logger
                        var errorUuid = Guid.NewGuid().ToString();
                        string message;
                        if (!env.IsDevelopment())
                            message = $"Internal error, a note in the system logs has been left with the id: {errorUuid}";
                        else
                            message = "::Exception message:: " + contextFeature.Error?.Message + "::Inner exception::" + contextFeature.Error?.InnerException?.Message;
                        //TODO - add the uuid to the logs
                        await context.Response.WriteAsync(new ExceptionDTO()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}