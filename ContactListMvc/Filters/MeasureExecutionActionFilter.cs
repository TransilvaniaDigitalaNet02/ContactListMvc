using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ContactListMvc.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MeasureExecutionActionFilter : Attribute, IActionFilter, IAsyncActionFilter
    {
        private Stopwatch _watch;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // executed before controller/action
            _watch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // executed after controller/action
            _watch.Stop();
            Console.WriteLine($"[sync] Executed {context.HttpContext.Request.Path} in {_watch.ElapsedMilliseconds} ms");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // code executed before controller/action
            Stopwatch asyncWatch = Stopwatch.StartNew();

            await next();

            // code executed after controller/action
            asyncWatch.Stop();
            Console.WriteLine($"[async] Executed {context.HttpContext.Request.Path} in {asyncWatch.ElapsedMilliseconds} ms");
        }
    }
}
