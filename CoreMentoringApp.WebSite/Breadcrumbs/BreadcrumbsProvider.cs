using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CoreMentoringApp.WebSite.Breadcrumbs
{
    public class BreadcrumbsProvider : IBreadcrumbsProvider
    {
        private readonly ILogger<BreadcrumbsProvider> _logger;

        public BreadcrumbsProvider(ILogger<BreadcrumbsProvider> logger)
        {
            _logger = logger;
        }

        public List<BreadcrumbItem> GetBreadcrumbs(ViewContext viewContext)
        {
            var routeDataValues = viewContext.RouteData.Values;
            var controller = Convert.ToString(routeDataValues["controller"]);
            var action = Convert.ToString(routeDataValues["action"]);
            var activeTitle = Convert.ToString(viewContext.ViewData["Title"]);
            var breadcrumbs = new List<BreadcrumbItem>();
            _logger.LogDebug("Prepare breadcrumbs for {action} action of {controller} controller.", action, controller);

            if (!controller.Equals("Home"))
            {
                breadcrumbs.Add(new BreadcrumbItem { Controller = "Home", Action = "Index", Title = "Home" });
            }

            if (action.Equals("Index"))
            {
                breadcrumbs.Add(new BreadcrumbItem { Controller = controller, Action = action, Title = string.IsNullOrEmpty(activeTitle) ? controller : activeTitle });
            }
            else
            {
                breadcrumbs.Add(new BreadcrumbItem { Controller = controller, Action = "Index", Title = controller });
                breadcrumbs.Add(new BreadcrumbItem { Controller = controller, Action = action, Title = string.IsNullOrEmpty(activeTitle) ? action : activeTitle });
            }

            breadcrumbs.Last().IsActive = true;

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Following breadcrumbs were provided:");
                foreach (var breadcrumbItem in breadcrumbs)
                {
                    var breadcrumb = JsonSerializer.Serialize(breadcrumbItem);
                    _logger.LogDebug("{breadcrumb}", breadcrumb);
                }
            }
            
            return breadcrumbs;
        }
    }
}