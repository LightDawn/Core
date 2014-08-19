using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Core.Mvc.Helpers.ElementAuthentication
{
    [Serializable]
    public class UserAccessibleElement
    {
        public static void DefineCrudActionAuthority(string url , AccessOperation crudOpt)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            else
            {
                var currentUser = System.Web.HttpContext.Current.User.Identity.Name;
                if (url.ToLower().StartsWith("api/"))
                {
                    var actualUrlName = url.Split('/')[1];

                    if (crudOpt.Insertable)
                    {
                        if (AppBase.HasCurrentUserAccess(currentUser, actualUrlName + "/PostEntitiy"))
                        {
                            crudOpt.Insertable = true;
                        }
                        else
                        {
                            crudOpt.Insertable = false;
                        }
                    }

                    if (crudOpt.Updatable)
                    {
                        if (AppBase.HasCurrentUserAccess(currentUser, actualUrlName + "/PutEntitiy"))
                        {
                            crudOpt.Updatable = true;
                        }
                        else
                        {
                            crudOpt.Updatable = false;
                        }
                    }

                    if (crudOpt.Removable)
                    {
                        if (AppBase.HasCurrentUserAccess(currentUser, actualUrlName + "/DeleteEntitiy"))
                        {
                            crudOpt.Removable = true;
                        }
                        else
                        {
                            crudOpt.Removable = false;
                        }
                    }
                }
                else
                {
                    //TODO: Must be implemented for classical controller. 
                    throw new NotImplementedException();
                }
            }
        }

        public static bool HasCustomActionAuthorized(string customActionName)
        {
            var currentUser = System.Web.HttpContext.Current.User.Identity.Name;
            return AppBase.HasCurrentUserAccess(currentUser, null, customActionName);
        }


    }
}
