using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Core.Cmn;
using System.Xml;


namespace Core.Mvc.Controller
{
    [ValidateInput(true)]
    public class ControllerBaseRP : System.Web.Mvc.Controller
    {

        protected override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            //if (filterContext.HttpContext.Request.Headers["X-Requested-With"] != null)
            //{
            //    if (filterContext.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            //        filterContext.Result = new JsonResult { RecursionLimit = 3, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.DenyGet };
            //}
            //else


            ExceptionInfo excepInfo = new ExceptionInfo(filterContext.Exception, false);
            
            if (!this.HttpContext.User.Identity.Name.ToLower().Equals("adminptacka"))
            {
                excepInfo.Message= Core.Resources.ExceptionMessage.ApplicationFaild;
                excepInfo.Details = "";
                excepInfo. IsRTL = true;
            }
            
            filterContext.Result = SetException(excepInfo);
            filterContext.ExceptionHandled = true;

            // log error by elmah

        }

        private ActionResult SetException(ExceptionInfo exception)
        {

            this.HttpContext.Response.Clear();
            
            this.HttpContext.Response.StatusCode = 500;

            this.HttpContext.Response.TrySkipIisCustomErrors = true;
            
            return new JsonResult
            {
                Data = new { Message = exception.Message, Details = exception.Details, IsRTL = exception.IsRTL }
                ,
                RecursionLimit = 3,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult ShowException(ExceptionInfo exception)
        {

            return SetException(exception);


        }

        public void AddModelError(List<ValidationResult> validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                ModelState.AddModelError(validationResult.ErrorMessage, ((string[])(validationResult.MemberNames))[0]);

            }

        }

        [HttpGet]
        public virtual ContentResult CreateHelpView(string viewModelName)
        {
            System.Xml.XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("~/Helpxml/Help.xml"));

            var viewModelHelp = doc.GetElementsByTagName(viewModelName);

            var result = string.Empty;

            foreach (XmlNode item in viewModelHelp)
            {
                result += item.InnerXml;
            }
            return Content(result);
        }
    }
}
