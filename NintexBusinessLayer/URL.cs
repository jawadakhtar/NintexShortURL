using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nintex.DataLayer;

namespace Nintex.BusinessLayer  
{
    /// <summary>
    /// Defines the associated URLs by the Client. 
    /// The design allows to have single instance of same URL, created by different clients
    /// </summary>
    public class URL : BusinessEntities
    {
        public string LongURL { get; set; }
        public string ShortURL { get; set; }

        /// <summary>
        /// This URL can be owned by several Clients
        /// </summary>
        public List<Client> Clients { get; set; }
        
        public URL()
        {
            Clients = new List<Client>();
        }

        public override void Add()
        {
            if (Nintex.TestContext.IsTesting == true)
            {
                this.Id = 1;
                GenerateShortURL(this, new BusinessLayer.URLShortener.Base64Shortener());
                return;
            }

            //CF-1 Insert in repository
            //CF-2 Generate the shorturl from generated Id
            //CF-3 update the URL in repository with generated shortUrl

            //CF-1
            //preparing the DataLayer URL
            DataLayer.URL dbURL = new DataLayer.URL();
            dbURL.LongURL = this.LongURL;
            dbURL.ShortURL = this.ShortURL;
            dbURL.Clients = new List<DataLayer.Client>();
            //For now we are having copy of same URL for all needy Clients. Later this can be optimized
            foreach (Client client in Clients)
            {
                dbURL.Clients.Add(new DataLayer.Client { Id = client.Id });
            }

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            db.URLs.Attach(dbURL);
            db.URLs.Add(dbURL);
            try
            {
                //saving the URL in repository to get its Id
                db.SaveChanges();
                
                Id = dbURL.Id;

                //CF-2
                //generating the short URL using the strategy pattern
                GenerateShortURL(this, new BusinessLayer.URLShortener.Base64Shortener());
                
                dbURL.ShortURL = this.ShortURL;

                //CF-3
                //updating the repository with generated short URL
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Deletes the URL
        /// </summary>
        public override void Delete()
        {
            if (Nintex.TestContext.IsTesting == true)
            {
                this.Id = 0;
                return;
            }

            //For now we are having copy of same URL for all needy Clients. Later this can be optimized
            var dbURL = new DataLayer.URL { Id = this.Id };

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            db.URLs.Attach(dbURL);
            db.URLs.Remove(dbURL);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }    

        private static void GenerateShortURL(URL aUrl, BusinessLayer.URLShortener.IURLShortenStrategy aShortner)
        {
            string shortURL = aShortner.GenerateShortURL(aUrl.LongURL, aUrl.Id);
            aUrl.ShortURL = shortURL;
        }

        /// <summary>
        /// Gets the Short URL against the LongURL, if the (any) client has created it within our system
        /// </summary>
        /// <param name="aLongURL"></param>
        /// <returns></returns>
        public static string QueryShortUrl(string aLongURL)
        {
            if (Nintex.TestContext.IsTesting == true)
            {
                return "TEST";
            }

            string shortUrl = "";

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            var longUrlRecord = db.URLs
                .Where(u => u.LongURL == aLongURL);

            if (longUrlRecord != null && longUrlRecord.Count() > 0)
                shortUrl = longUrlRecord.First().ShortURL;

            return shortUrl;
        }

        /// <summary>
        /// Gets the Long url from short URL, if it is generated from our system
        /// </summary>
        /// <param name="aShortUrlId"></param>
        /// <returns></returns>
        public static string QueryLongUrl(string aShortUrlId)
        {
            if (Nintex.TestContext.IsTesting == true)
            {
                return "www.google.com";
            }

            string longUrl = "";

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            var longUrlRecord = db.URLs
                .Where(u => u.ShortURL == aShortUrlId);

            if (longUrlRecord != null && longUrlRecord.Count() > 0)
                longUrl = longUrlRecord.First().LongURL;  

            return longUrl;
        }

    }
}