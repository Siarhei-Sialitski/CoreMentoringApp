using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMentoringApp.WebSite.Breadcrumbs
{
    public interface IBreadcrumbsProvider
    {
        List<BreadcrumbItem> GetBreadcrumbs(ViewContext viewContext);
    }
}
