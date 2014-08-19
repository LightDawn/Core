using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Core.Mvc.Helpers
{
    public static class LabelHelper
    {
        #region LabelRP
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression, string text, IDictionary<string, object> htmlAttributes, string cssClass)
        {
            htmlAttributes.Add("Style", cssClass);
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression, text, htmlAttributes);
        }
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression, string text, object htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression, text, htmlAttributes);
        }
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression, IDictionary<string, object> htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression, string text)
        {
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression, text);
        }
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression, object htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString LabelRP(this HtmlHelper helper, string expression)
        {
            return System.Web.Mvc.Html.LabelExtensions.Label(helper, expression);
        }
        #endregion

        #region LabelRPFor<TModel, TValue>
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression, string text, IDictionary<string, object> htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression, text, htmlAttributes);
        }
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression, string text, object htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression, text, htmlAttributes);
        }
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression, string text)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression, text);
        }
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression, htmlAttributes);
        }
        public static MvcHtmlString LabelRPFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TValue>> expression)
        {
            return System.Web.Mvc.Html.LabelExtensions.LabelFor<TModel, TValue>(helper, expression);
        }
        #endregion
    }
}
