using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMentoringApp.WebSite.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper htmlHelper, int imageId, string linkText)
        {
            TagBuilder aTagBuilder = new TagBuilder("a");
            aTagBuilder.Attributes.Add("href", $"images/{imageId}");
            aTagBuilder.InnerHtml.Append(linkText);

            var writer = new StringWriter();
            aTagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}