using CoreMentoringApp.WebSite.Filters.CustomActionLogger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreMentoringApp.WebSite.Filters
{
    public class CustomizedLoggingActionFilter : IActionFilter
    {
        private readonly ICustomActionLogger _customActionLogger;

        public CustomizedLoggingActionFilter(ICustomActionLogger customActionLogger)
        {
            _customActionLogger = customActionLogger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _customActionLogger.ActionStartLog(context);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _customActionLogger.ActionEndLog(context);
        }
    }
}
