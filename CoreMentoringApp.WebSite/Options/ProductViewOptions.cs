using System.ComponentModel.DataAnnotations;

namespace CoreMentoringApp.WebSite.Options
{
    public class ProductViewOptions
    {
        public const string ProductView = "ProductView";

        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}
