using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Nintex.BusinessLayer
{
    /// <summary>
    /// A business entity for Client
    /// </summary>
    public class Client : BusinessEntities
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<URL> URLs { get; set; }

        /// <summary>
        /// A ctor to initialize the Client
        /// </summary>
        public Client()
        {
            IsAuthenticated = false;
            Subscriptions = new List<Subscription>();
            URLs = new List<URL>();
        }

        /// <summary>
        /// Authenticates the Client against LoginId and Password
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            try
            {
                var user = GetClient(this.LoginId, this.Password);
                if (user != null)
                {
                    IsAuthenticated = true;
                    Id = user.Id;
                }
            }
            catch (Exception ex)
            {
                IsAuthenticated = false;
                throw ex;
            }
            finally
            {
                Password = "";
            }
            return true;
        }

        /// <summary>
        /// Returns the client having provided LoginId and Password
        /// </summary>
        /// <param name="aLoginId"></param>
        /// <param name="aPassword"></param>
        /// <returns></returns>
        public static Client GetClient(string aLoginId, string aPassword)
        {
            //in case of unit-testing
            if (Nintex.TestContext.IsTesting == true)
            {
                return new Client
                {
                    Id = 1,
                    LoginId = aLoginId,
                    Name = aLoginId
                };
            }

            Client client = new Client();
            string pwd = Security.HashPassword(aPassword);
            Nintex.DataLayer.NintexUrlDbEntities db = new DataLayer.NintexUrlDbEntities();
            client = db.Clients
                .Where(c => c.LoginId == aLoginId && c.PasswordHash == pwd)
                .Select(c => new BusinessLayer.Client
                {
                    Id = c.Id,
                    LoginId = c.LoginId,
                    Name = c.Name,                    
                }).Single();
            return client;
        }

        /// <summary>
        /// Adds the Client in repository
        /// </summary>
        private new void Add()
        {
            //in case of unit-testing
            if (Nintex.TestContext.IsTesting == true)
            {
                this.Id = 1;
                return;
            }

            DataLayer.Client newClient = new DataLayer.Client();
            newClient.Name = this.LoginId;
            newClient.LoginId = this.LoginId;
            newClient.PasswordHash = Security.HashPassword(this.Password);

            DataLayer.NintexUrlDbEntities db = new DataLayer.NintexUrlDbEntities();
            db.Clients.Add(newClient);
            //db.Entry(newClient);
            db.SaveChanges();

            this.Id = newClient.Id;
        }

        /// <summary>
        /// Registers the New Client if it doesn't already exist
        /// </summary>
        public void Register()
        {
            //CF-1: Check existing client
            //CF-2: Register new Client

            //CF-1: Check existing client
            bool clientExist = false;
            try
            {
                clientExist = GetClient(this.LoginId, this.Password) == null ? false : true;
            }
            catch { 
                //ignoring the exception 
            }

            if (IsAuthenticated == true || clientExist == true && Nintex.TestContext.IsTesting == false)
            {
                Password = "";
                throw new ApplicationException("User already registered");
            }

            //CF-2: Register new Client
            try
            {
                Add();
            }
            catch (Exception ex)
            {
                this.IsAuthenticated = false;

                if (ex.Message.ToLower().Contains("cannot insert duplicate key"))
                    throw new ApplicationException("Client already exist with Login ID " + this.LoginId);

                throw ex;
            }
            finally
            {
                this.Password = null;
            }

            this.IsAuthenticated = true;
        }

        /// <summary>
        /// Get the URLs added by the Client
        /// </summary>
        public void GetURLs()
        {
            URLs.Clear();

            if(Nintex.TestContext.IsTesting == true)
            {
                URLs.Add(new URL { Id = 1, LongURL = "www.google.com", ShortURL = "TEST" });
                return;
            }

            Nintex.DataLayer.Client dbClient = new DataLayer.Client { Id = this.Id };
            try
            {
                DataLayer.NintexUrlDbEntities db = new DataLayer.NintexUrlDbEntities();
                URLs = db.VWClientURLs
                    .Where(u => u.ClientId == this.Id)
                    .Select(u => new BusinessLayer.URL
                    {
                        Id = u.UrlId,
                        LongURL = u.LongURL,
                        ShortURL = u.ShortURL
                    }).ToList<BusinessLayer.URL>();
            }
            catch(Exception ex)
            {
                //ignore the exception
            }
        }

        /// <summary>
        /// Initializes the Subcriptions
        /// </summary>
        /// <returns></returns>
        public List<Subscription> GetSubscriptions()
        {
            Subscriptions = new List<Subscription> { new Subscription { Client = this, Package = new Package { Id = 1, Charges = 1000, Type = Package.PackageType.Monthly }, SubscriptionDate = DateTime.Now.AddDays(-20) },
                                            new Subscription { Client = this, Package = new Package { Id = 2, Charges = 12000, Type = Package.PackageType.Yearly }, SubscriptionDate = DateTime.Now.AddDays(-10) }
                                            };
            return Subscriptions;
        }
    }
}