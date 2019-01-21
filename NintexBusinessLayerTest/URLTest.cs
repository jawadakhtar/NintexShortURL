using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nintex.BusinessLayer;

namespace NintexBusinessLayerTest
{
    [TestClass]
    public class URLTest
    {
        static URLTest()
        {
            Nintex.TestContext.IsTesting = true;
        }
            
        static URL CreateURL()
        {
            URL url = new URL { Id = 0, LongURL = "www.google.com" };
            Client client = ClientTest.CreateClient();
            client.Register();

            url.Clients.Add(client);
            return url;
        }


        [TestMethod]
        public void AddURLTest()
        {
            URL url = CreateURL();
            url.Add();

            Assert.IsTrue(url.Id > 0);
        }

        [TestMethod]
        public void DeleteURLTest()
        {
            URL url = CreateURL();
            url.Add();

            Assert.IsTrue(url.Id > 0);

            url.Delete();
            Assert.IsTrue(url.Id == 0);
        }


    }
}
