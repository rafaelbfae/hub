﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CrmHub.Web
{
    public static class HtmlHelpers
    {

        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string ToJson<T>(this T @this)
        {
            return JsonConvert.SerializeObject(@this);
        }

        public static T ToObject<T>(this string @this)
        {
            return JsonConvert.DeserializeObject<T>(@this);
        }

    }
}
