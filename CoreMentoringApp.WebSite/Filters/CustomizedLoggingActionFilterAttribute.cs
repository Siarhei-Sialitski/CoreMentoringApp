using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Filters
{
    public class CustomizedLoggingActionFilterAttribute : TypeFilterAttribute
    {
        public CustomizedLoggingActionFilterAttribute() : base(typeof(CustomizedLoggingActionFilter))
        {
        }
    }
}
