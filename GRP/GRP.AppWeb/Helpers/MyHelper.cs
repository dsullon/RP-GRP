﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;

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

        public static MvcHtmlString DisplayWithIdFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string wrapperTag = "div")
        {
            var id = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(string.Format("<{0} id=\"{1}\">{2}</{0}>", wrapperTag, id, helper.DisplayFor(expression)));
        }

        public static MvcHtmlString ActionLinkMenu(this HtmlHelper htmlHelper, Func<object, HelperResult> template, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            return ActionLinkMenu(htmlHelper, template(null).ToString(), actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkMenu(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.ActionLinkRaw(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString()
            };

            if (controllerName == currentController && actionName == currentAction)
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }

        private static MvcHtmlString ActionLinkRaw(this HtmlHelper htmlHelper, string rawHtml, string actionName, string controllerName, object routeValues, object htmlAttributes = null)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = htmlHelper.ActionLink(repID, actionName, controllerName, routeValues, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, rawHtml));
        }

        public static bool Contains(this string input, string find, StringComparison comparisonType)
        {
            return String.IsNullOrWhiteSpace(input) ? false : input.IndexOf(find, comparisonType) > -1;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> expression)
        {
            if (condition)
                return source.Where(expression);
            else
                return source;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string prop, string order)
        {
            var type = typeof(T);
            var property = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
                order.Equals("asc", StringComparison.InvariantCultureIgnoreCase) ? "OrderBy" : "OrderByDescending",
                new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}