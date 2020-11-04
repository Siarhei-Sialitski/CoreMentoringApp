using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreMentoringApp.WebSite.Filters.CustomActionLogger
{
    public interface ICustomActionLogger
    {
        void ActionStartLog(ActionExecutingContext context);
        void ActionEndLog(ActionExecutedContext context);
    }
}
