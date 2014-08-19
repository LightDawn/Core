using Core.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Mvc.Helpers;
using Kendo.Mvc.Infrastructure;
using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Kendo.Mvc.UI;
namespace Core.Mvc.Helpers
{
    public static class TreeViewHelper
    {

        public static MvcHtmlString TreeViewRP(this HtmlHelper helper, TreeInfo info, string name, TreeViewEventBuilder events, bool hasCheckBox)
        {
            info.DataSource.ModelRP.ModelType = typeof(TreeViewModelBase);

            var initializer = DI.Current.Resolve<IJavaScriptInitializer>();

            TreeView tree;

            info.Name = name;

            tree = new TreeView(helper.ViewContext, initializer, info, hasCheckBox);

            if (events != null)
            {
                tree.Events = events.handler;
            }

            tree.DataTextField = info.DataTextField;

            var builder = new TreeViewBuilder(tree);

            return MvcHtmlString.Create(builder.ToHtmlString());

        }

        public static MvcHtmlString TreeViewRP(this HtmlHelper helper, TreeInfo info, string name, bool hasCheckBox)
        {

            return TreeViewRP(helper, info, name, null, hasCheckBox);

        }
   
        public static MvcHtmlString TreeViewRP<TModel>(this HtmlHelper helper, TreeInfo info, string name, TreeViewEventBuilder events, bool hasCheckBox) where TModel : IViewModelBase, new()
        {
            info.DataSource.ModelRP.ModelType = typeof(TModel);

            var initializer = DI.Current.Resolve<IJavaScriptInitializer>();

            info.Name = name;

            TreeView tree = new TreeView(helper.ViewContext, initializer, info, hasCheckBox);

            tree.Events = events.handler;

            tree.DataTextField = info.DataTextField;

            var builder = new TreeViewBuilder(tree);

            return MvcHtmlString.Create(builder.ToHtmlString());

        }


        public static MvcHtmlString TreeViewRP<TModel>(this HtmlHelper helper, TreeInfo info, string name, TreeViewEventBuilder events) where TModel : IViewModelBase, new()
        {
            return TreeViewRP<TModel>(helper, info, name, events, false);

        }

    }
}
