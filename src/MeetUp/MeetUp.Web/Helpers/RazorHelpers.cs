using System.Web.Mvc;

namespace MeetUp.Web.Helpers
{
    public static class RazorHelpers
    {
        public static MvcHtmlString BtnIconLink(this HtmlHelper helper, string text, string action, string controllerName, object routeValues, string icon, string title, string btnclasses = "")
        {
            var i = new TagBuilder("i");
            i.MergeAttribute("class", "fa " + icon);
            i.MergeAttribute("Title", title);
            var iHtml = i.ToString(TagRenderMode.Normal);

            var url = new UrlHelper(helper.ViewContext.RequestContext);
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controllerName, routeValues));
            anchorBuilder.MergeAttribute("class", btnclasses);
            anchorBuilder.InnerHtml = iHtml + " " + text;
            var anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        } 
    }
}