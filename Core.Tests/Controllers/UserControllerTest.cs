using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProject;
using SampleProject.Controllers.User;
using Moq;
using Core.Service;
using Core.Model;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Core.Mvc.ViewModel;



namespace Core.Tests.Controllers
{   
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IServiceBase<User>> _mockUserService;
       /// <summary>
       /// 
       /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mockUserService = new Mock<IServiceBase<User>>();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            var mockService = _mockUserService;
            var user = new User { Id = 1, FName = "Behnam", LName = "Alavi", UserName = "b-alavi" };
            mockService.Setup(cr => cr.Create(user)).Returns(user);
            var controller = new UserController(mockService.Object);
            // Act
            var userViewModel = new UserViewModel(user);// { Id = user.Id, FName = user.FName, LName = user.LName };
            controller.ModelState.Clear();
            var result = (JsonResult)controller.Create(new DataSourceRequest() { PageSize=3 , Page=0 }, userViewModel);
            var resDataCount = ((result.Data as Kendo.Mvc.UI.DataSourceResult).Data as System.Collections.Generic.List<UserViewModel>).Count;
            var resDataFName = ((result.Data as Kendo.Mvc.UI.DataSourceResult).Data as System.Collections.Generic.List<UserViewModel>).First().FName;
            // Assert
            // Assert.AreEqual("1", resDataCount.ToString());
            Assert.AreEqual(resDataFName, user.FName);
        }

        [TestMethod]
        public void ReadTest()
        {
            // Arrange
            var mockService = _mockUserService;
            User[] users = new[] { new User { Id = 1, FName = "Behnam", LName = "Alavi", UserName = "b-alavi" } };
            mockService.Setup(cr => cr.Load()).Returns(users);
            var controller = new UserController(mockService.Object);
            // Act
            var result = (JsonResult)controller.Read(new DataSourceRequest());
            var resDataCount = ((result.Data as Kendo.Mvc.UI.DataSourceResult).Data as System.Collections.Generic.List<UserViewModel>).Count;
            var resDataFName = ((result.Data as Kendo.Mvc.UI.DataSourceResult).Data as System.Collections.Generic.List<UserViewModel>).First().FName;
            // Assert
            Assert.AreEqual("1", resDataCount.ToString());
            Assert.AreEqual("Behnam", resDataFName);
        }

        [TestMethod]
        public void EditTest()
        {
            // Arrange
            var mockService = _mockUserService;
            var user = new User { Id = 1, FName = "Bahram", LName = "Havazi", UserName = "b-havazi" };
            var retValue = 0;
            mockService.Setup(cr => cr.Update(user)).Returns(retValue);
            var controller = new UserController(mockService.Object);
            // Act
            var userViewModel = new UserViewModel(user);
            controller.ModelState.Clear();
            var result = (JsonResult)controller.Update(new DataSourceRequest(), userViewModel);
            retValue = (((Kendo.Mvc.UI.DataSourceResult)(result.Data)).Data as System.Collections.Generic.List<int>).First();
            // Assert
            Assert.AreEqual(0, retValue);
        }
        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            var mockService = _mockUserService;
            var user = new User { Id = 1, FName = "Bahram", LName = "Havazi", UserName = "b-havazi" };
            var retValue = 0;
            mockService.Setup(cr => cr.Delete(user)).Returns(retValue);
            var controller = new UserController(mockService.Object);
            // Act
            var userViewModel = new UserViewModel(user);
            controller.ModelState.Clear();
            var result = (JsonResult)controller.Delete(new DataSourceRequest(), userViewModel);
            retValue = (((Kendo.Mvc.UI.DataSourceResult)(result.Data)).Data as System.Collections.Generic.List<int>).First();
            //var resDataCount = (int)(result.Data as Kendo.Mvc.UI.DataSourceResult).Data;
            // Assert
            //Assert.AreEqual("1", resDataCount.ToString());
            Assert.AreEqual(0, retValue);
            //Assert.IsFalse(retValue == 0);
        }
    }
}
