using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMentoringApp.WebSite.Breadcrumbs;
using CoreMentoringApp.WebSite.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;

namespace CoreMentoringApp.WebSite.Tests.ViewComponents
{
    public class BreadcrumbsViewComponentTests
    {
        [Fact]
        public async Task InvokeAsync_ReturnsViewWithRequestedFromProviderBreadcrumbs()
        {
            var mockProvider = new Mock<IBreadcrumbsProvider>();
            var context = new ViewContext();
            var breadcrumbs = new List<BreadcrumbItem>();

            mockProvider.Setup(m => m.GetBreadcrumbs(context))
                .Returns(breadcrumbs)
                .Verifiable();
            var viewComponent = new BreadcrumbsViewComponent(mockProvider.Object);

            var result = await viewComponent.InvokeAsync(context);

            var viewResult = Assert.IsType<ViewViewComponentResult>(result);
            var resultModel = Assert.IsAssignableFrom<List<BreadcrumbItem>>(viewResult.ViewData.Model);
            Assert.Equal(breadcrumbs, resultModel);
            mockProvider.Verify();
        }
    }
}
