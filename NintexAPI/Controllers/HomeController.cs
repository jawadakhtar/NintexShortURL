using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NintexAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            string longUrl = Nintex.BusinessLayer.URL.QueryLongUrl(id);

            if (string.IsNullOrEmpty(longUrl) == false)
                return RedirectPermanent(longUrl);
            else
            {
                NintexAPI.Models.APIModel apiModel = new Models.APIModel();
                apiModel.URLFound = false;  
                return View(apiModel);
            }            
        }

        public ActionResult ToShort(string longURL)
        {
            //    string shortUrl = Nintex.BusinessLayer.URL.QueryShortUrl(longURL);

            //    NintexAPI.Models.APIModel apiModel = new Models.APIModel();
            //    apiModel.URLFound = false;

            //    if (string.IsNullOrEmpty(shortUrl) == false)
            //    {
            //        apiModel.URLFound = true;
            //        apiModel.ShortURL = shortUrl;
            //        return View(apiModel);
            //    }
            //    else
            //    {
            //        return View(apiModel);
            //    }
            //}
            return View();
        }
    }
}
