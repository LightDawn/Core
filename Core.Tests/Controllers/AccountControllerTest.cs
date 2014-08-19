using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
//using SampleProject.Controllers.Account;
using Microsoft.QualityTools.Testing.Fakes;
//using System.Fakes;

namespace Core.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void TestLogOff()
        {
            //var accountController = new AccountController(new ControlPanelAuthentication());
            RedirectToRouteResult redirectToRouteResult;

            //Scope the detours we're creating
            using (ShimsContext.Create())
            {
                //Detours FormsAuthentication.SignOut() to an empty implementation
                //ShimFormsAuthentication.SignOut = () => { };
                //redirectToRouteResult = accountController.LogOff() as RedirectToRouteResult;
            }

            //Assert.IsNotNull(redirectToRouteResult);
            //Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            //Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);
        }

    }
}
