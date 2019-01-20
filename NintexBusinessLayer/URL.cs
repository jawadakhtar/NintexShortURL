using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nintex.DataLayer;

namespace Nintex.BusinessLayer  
{
    public class URL : BusinessEntities
    {
        public string LongURL { get; set; }
        public string ShortURL { get; set; }

        public List<Client> Clients { get; set; }
        
        public URL()
        {
            Clients = new List<Client>();
        }

        public override void Add()
        {
            DataLayer.URL dbURL = new DataLayer.URL();
            dbURL.LongURL = this.LongURL;
            dbURL.ShortURL = this.ShortURL;
            dbURL.Clients = new List<DataLayer.Client>();
            foreach (Client client in Clients)
            {
                dbURL.Clients.Add(new DataLayer.Client { Id = client.Id });
            }

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            db.URLs.Attach(dbURL);
            db.URLs.Add(dbURL);
            try
            {
                db.SaveChanges();

                Id = dbURL.Id;

                GenerateShortURL(new BusinessLayer.URLShortener.Base64Shortener());
                
                dbURL.ShortURL = this.ShortURL;

                db.SaveChanges();

                //Update();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Update()
        {
            if (Id == 0)
            {
                throw new ApplicationException("URL doesn't exist in repository. Add the record first.");
            }

            DataLayer.URL dbURL = new DataLayer.URL();
            dbURL.Id = this.Id;
            dbURL.LongURL = this.LongURL;
            dbURL.ShortURL = this.ShortURL;
            
            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            db.URLs.Attach(dbURL);
            db.URLs.Add(dbURL);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Delete()
        {
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

        private void GenerateShortURL(BusinessLayer.URLShortener.IURLShortenStrategy aShortner)
        {
            string shortURL = aShortner.GenerateShortURL(LongURL, Id);
            ShortURL = shortURL;
        }


        public static string QueryShortUrl(string aLongURL)
        {
            string shortUrl = "";

            DataLayer.NintexUrlDbEntities db = new NintexUrlDbEntities();
            var longUrlRecord = db.URLs
                .Where(u => u.LongURL == aLongURL);

            if (longUrlRecord != null && longUrlRecord.Count() > 0)
                shortUrl = longUrlRecord.First().ShortURL;

            return shortUrl;
        }

        public static string QueryLongUrl(string aShortUrlId)
        {
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