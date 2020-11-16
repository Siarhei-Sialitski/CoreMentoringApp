using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Breadcrumbs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMentoringApp.WebSite.ViewComponents
{
    [ViewComponent]
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly IBreadcrumbsProvider _breadcrumbsProvider;

        public BreadcrumbsViewComponent(IBreadcrumbsProvider breadcrumbsProvider)
        {
            _breadcrumbsProvider = breadcrumbsProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync(ViewContext context)
        {
            var breadcrumbs = await Task.Run(() => _breadcrumbsProvider.GetBreadcrumbs(context));
            return View(breadcrumbs);
        }

    }
}
