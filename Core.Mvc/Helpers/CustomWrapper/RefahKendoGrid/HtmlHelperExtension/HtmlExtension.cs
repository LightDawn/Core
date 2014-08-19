using Core.Model;
using Core.Mvc.Helpers.RefahKendoGrid.Settings;
using Core.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Core.Mvc.Helpers.RefahKendoGrid
{
    public static class HtmlExtension
    {
        public static MvcHtmlString GridRP(this HtmlHelper helper ,string gridName, GridInfo gridTotalConfig , string Width = null , string Height = null)// where T : class
        {
            return (new GridRP<object>(helper , gridName, gridTotalConfig ,null, null , Width , Height)).Render();
        }
        public static MvcHtmlString GridRP(this HtmlHelper helper, string gridName, GridInfo gridTotalConfig, ClientDependentFeature clientDependency, string Width = null, string Height = null)// where T : class
        {
            return (new GridRP<object>(helper, gridName, gridTotalConfig, null, clientDependency, Width, Height)).Render();
        }
        public static MvcHtmlString GridRP<T>(this HtmlHelper helper, string gridName, GridInfo gridTotalConfig, string Width = null, string Height = null) where T : IViewModelBase, new()
        {
            return (new GridRP<T>(helper, gridName, gridTotalConfig, null , null, Width , Height)).Render();
        }
        public static MvcHtmlString GridRP<T>(this HtmlHelper helper, string gridName, GridInfo gridTotalConfig, ClientDependentFeature clientDependency, string Width = null, string Height = null) where T : IViewModelBase, new()
        {
            return (new GridRP<T>(helper , gridName, gridTotalConfig, null,  clientDependency , Width , Height)).Render();
        }

        public static MvcHtmlString GridRP(this HtmlHelper helper, string gridName, GridInfo gridTotalConfig, Type viewModelType , ClientDependentFeature clientDependency, string Width = null, string Height = null)
        {
            //var moeinAsm =  AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => asm.FullName.Contains("Moein.Mvc"));

            return (new GridRP<object>(helper, gridName, gridTotalConfig, viewModelType , clientDependency, Width, Height)).Render();
        }


        //public static string GetKendoGridRpScripts(this HtmlHelper helper)
        //{
        //    EmbeddedResourceVirtualPathProvider.EmbeddedResource fgh = new EmbeddedResourceVirtualPathProvider.EmbeddedResource(System.Reflection.Assembly.GetAssembly(typeof(GridRP<object>)), "Core.Mvc.Scripts.KendoGridRp.js", "Core.Mvc.Scripts.KendoGridRp.js");
        //    return fgh.ResourcePath;
        //}
    }
}
