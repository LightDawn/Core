using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Core.Mvc.Attribute
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext actionContext)
        {
           // string str=string.Empty;
           // var exceptionType = actionContext.Exception.GetType();
           // if (exceptionType == typeof(UnauthorizedAccessException))
           // {
              //  Exception exp =new Exception(  "دسترسی نداری پلووو",actionContext.Exception.InnerException);
               
          //  }
            base.OnException(actionContext);

            //((System.Web.HttpContextWrapper)(actionContext.Request.Properties["MS_HttpContext"])).User
            var context = actionContext.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            
            Exception exp = actionContext.Exception;

            if (!context.User.Identity.Name.ToLower().Equals("adminptacka"))
            {
                 exp = new Exception(Core.Resources.ExceptionMessage.ApplicationFaild);
            }

            actionContext.Response = actionContext.Request.CreateErrorResponse
                (System.Net.HttpStatusCode.InternalServerError, exp);
            
            //actionContext.Exception = null;
            //actionContext.Request.DisposeRequestResources();
            //actionContext.Response.Headers.Location = new Uri(actionContext.Request.RequestUri.Authority);

            //HttpResponseMessage responseMsg = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            //responseMsg.Content =new StringContent( actionContext.Exception.Message);// new StringContent("ddddd");
            //responseMsg.ReasonPhrase = "test";
            //actionContext.Response = responseMsg;


            // throw new HttpResponseException(responseMsg);
            //throw new HttpException("دسترسی نداری پلووو");
        }
       
    }
}
