using Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private IWebHostEnvironment environment;

         public ExceptionMiddleware(RequestDelegate _next, IWebHostEnvironment _environment,ILogger<ExceptionMiddleware> _logger)
        {
            this.next = _next;
            this.environment = _environment;
            this.logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (System.Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, System.Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";

            void ReadException(System.Exception ex)
            {
                error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }
            ReadException(e);
            if (environment.IsDevelopment())
            {
                var json = new { code = 500, message = e.Message, detail = error };
              
                error = Other.JsonToString(json);
                logger.LogError(e,"系统异常");

            }
            else
                error = "抱歉，出错了";
            await context.Response.WriteAsync(error);
        }
    }
}
