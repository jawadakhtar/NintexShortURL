using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NintexAPI;
using NintexAPI.Controllers;

namespace NintexAPI.Tests.Controllers
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
            RedirectResult result = controller.Index("TEST") as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Url));
        }

        [TestMethod]
        public void ToShort()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ToShort("www.google.com") as ViewResult;
            var model = result.Model as NintexAPI.Models.APIModel;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.IsFalse(string.IsNullOrEmpty(model.ShortURL));


        }
    }
}
