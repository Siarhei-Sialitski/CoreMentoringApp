using System;
using System.IO;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Cache;
using CoreMentoringApp.WebSite.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoreMentoringApp.WebSite.Middlewares
{
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CacheMiddleware> _logger;
        private readonly IStreamMemoryCacheWorker _streamMemoryCacheWorker;

        public CacheMiddleware(RequestDelegate next,
            ILogger<CacheMiddleware> logger,
            IStreamMemoryCacheWorker streamMemoryCacheWorker)
        {
            _next = next;
            _logger = logger;
            _streamMemoryCacheWorker = streamMemoryCacheWorker;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("image"))
            {
                _logger.LogDebug("Trying to find image in cache location.");
                string id = Convert.ToString(context.Request.RouteValues["id"]);
                if (!string.IsNullOrEmpty(id))
                {
                    try
                    {
                        using (Stream str = _streamMemoryCacheWorker.GetStreamMemoryCacheValue(id))
                        {
                            context.Response.ContentType = "image/jpg";
                            await str.CopyToAsync(context.Response.Body);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(LogEvents.HandledException, ex, "Exception occured during loading image {id} from cache.", id);
                    }

                    try
                    {
                        using (MemoryStream responseBodyStream = new MemoryStream())
                        {
                            Stream originalResponse = context.Response.Body;
                            context.Response.Body = responseBodyStream;

                            await _next(context);

                            responseBodyStream.Seek(0, SeekOrigin.Begin);
                            _streamMemoryCacheWorker.SetStreamMemoryCacheValue(id, responseBodyStream);

                            responseBodyStream.Seek(0, SeekOrigin.Begin);
                            context.Response.Body = originalResponse;
                            await responseBodyStream.CopyToAsync(originalResponse);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(LogEvents.HandledException, ex, "Exception occured during caching image {id}.", id);
                    }
                }
                _logger.LogDebug("Trying to find image in cache location.");
            }
            await _next(context);
            
        }

    }

    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseCacheMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheMiddleware>();
        }
    }

}
