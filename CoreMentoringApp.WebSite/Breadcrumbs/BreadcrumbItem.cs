namespace CoreMentoringApp.WebSite.Breadcrumbs
{
    public class BreadcrumbItem {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
