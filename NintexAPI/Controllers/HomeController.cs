using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NintexAPI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the Long URL from the provided short Id and redirects the requestee to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            string longUrl = Nintex.BusinessLayer.URL.QueryLongUrl(id);

            if (string.IsNullOrEmpty(longUrl) == false)
                return RedirectPermanent("http://" + longUrl);
            else
            {
                NintexAPI.Models.APIModel apiModel = new Models.APIModel();
                apiModel.URLFound = false;  
                return View(apiModel);
            }            
        }

        /// <summary>
        /// Queries the short url code from the provided long URL
        /// </summary>
        /// <param name="longURL"></param>
        /// <returns></returns>
        public ActionResult ToShort(string longURL)
        {
            string shortUrl = Nintex.BusinessLayer.URL.QueryShortUrl(longURL);

            NintexAPI.Models.APIModel apiModel = new Models.APIModel();
            apiModel.URLFound = false;

            if (string.IsNullOrEmpty(shortUrl) == false)
            {
                apiModel.URLFound = true;
                apiModel.ShortURL = shortUrl;
                return View(apiModel);
            }
            else
            {
                return View(apiModel);
            }
        }
    }
}
