using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nintex.BusinessLayer;

namespace NintexBusinessLayerTest
{
    [TestClass]
    public class ClientTest
    {
        static ClientTest()
            {
            Nintex.TestContext.IsTesting = true;
        }

        public static Client CreateClient()
        {
            Client client = new Client();
            client.LoginId = "test";
            client.Password = "pass";
            return client;
        }

        [TestMethod]
        public void LoginTest()
        {
            Client client = new Client();
            client.LoginId = "test";
            client.Password = "pass";

            client.Login();

            Assert.IsTrue(client.IsAuthenticated == true);
        }


        [TestMethod]
        public void GetClientTest()
        {
            Client client = CreateClient();
            client.Register();
            var newClient = Client.GetClient(client.LoginId, "pass");

            Assert.IsNotNull(newClient);
        }

        [TestMethod]
        public void RegisterClientTest()
        {
            var client = CreateClient();
            client.Register();

            Assert.IsTrue(client.Id > 0);
            Assert.IsTrue(client.IsAuthenticated == true);
        }


        [TestMethod]
        public void GetURLClientTest()
        {
            var client = CreateClient();
            client.Register();
            client.Login();
            client.URLs.Add( new URL { LongURL = "www.google.com" });
            client.URLs[0].Add();            

            client.GetURLs();

            Assert.IsTrue(client.URLs.Count > 0);
        }


    }
}
