using System.Web.Mvc;
using System.Web.Routing;

namespace GRP.AppWeb
{
    public static class MyHelper
    {
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
            string text, string title, string action, string controller,
            object routeValues = null, object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }

        public static dynamic GetPageViewBag(this HtmlHelper html)
        {
            if (html == null || html.ViewContext == null) //this means that the page is root or parial view
                return html.ViewBag;

            ControllerBase controller = html.ViewContext.Controller;
            while (controller.ControllerContext.IsChildAction)  //traverse hierachy to get root controller
                controller = controller.ControllerContext.ParentActionViewContext.Controller;

            return controller.ViewBag;
        }
    }
}