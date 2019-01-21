using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nintex;
using Nintex.Controllers;

namespace Nintex.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        static HomeControllerTest()
        {
            Nintex.TestContext.IsTesting = true;
        }


        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("This is Nintex Test Application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Jawad Mohsin Akhtar.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Register()
        {
            HomeController controller = new HomeController();

            var registerResult = controller.Register("test", "test", "test") as ViewResult;
            // Assert
            Assert.IsNotNull(registerResult);
            Assert.IsNotNull(registerResult.Model);


            ActionResult loginResult = controller.Index("test", "test") as ActionResult;

            Assert.IsNotNull(loginResult);
        }
    }
}
