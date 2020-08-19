using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Middleware
{
    /// <summary>
    /// 请求耗时中间件
    /// </summary>
    public class RequestTimeMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;

        private readonly RequestDelegate _next;

        public RequestTimeMiddleware(RequestDelegate next, ILogger<RequestTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            context.Response.OnStarting(() =>
            {
                stopWatch.Stop();
                var requestTime = stopWatch.ElapsedMilliseconds;
                context.Response.Headers["X-Request-Time"] = requestTime.ToString();

                if (requestTime >= 1000)
                {
                    _logger.LogDebug($"请求链接：{context.Request.Path}，耗时({requestTime}ms)");
                }

                return Task.CompletedTask;
            });

            return _next(context);
        }
    }
}
