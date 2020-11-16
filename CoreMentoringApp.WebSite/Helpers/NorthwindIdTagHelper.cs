using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreMentoringApp.WebSite.Helpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class NorthwindIdTagHelper : TagHelper
    {
        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("href", $"images/{NorthwindId}");
        }
    }
}
