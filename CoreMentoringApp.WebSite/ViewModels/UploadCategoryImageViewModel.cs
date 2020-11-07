using Microsoft.AspNetCore.Http;

namespace CoreMentoringApp.WebSite.ViewModels
{
    public class UploadCategoryImageViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
