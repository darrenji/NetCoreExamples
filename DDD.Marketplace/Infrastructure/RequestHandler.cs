using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Marketplace.Infrastructure
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleCommand<T>(T request, Func<T, Task> handler, ILogger logger)
        {
            try
            {
                logger.LogDebug("Handling Http request of type {type}", typeof(T).Name);
                await handler(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error handling the command");
                return new BadRequestObjectResult(new { error=ex.Message, stackTrace=ex.StackTrace });
            }
        }

        public static async Task<IActionResult> HandleQuery<TModel>(Func<Task<TModel>> query, ILogger logger)
        {
            try
            {
                return new OkObjectResult(await query());
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Error handling the query");
                return new BadRequestObjectResult(new { error=ex.Message, stackTrace = ex.StackTrace});
            }
        }
    }
}
