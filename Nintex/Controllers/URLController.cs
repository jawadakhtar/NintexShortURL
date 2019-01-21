using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nintex.Controllers
{
    public class URLController : Controller
    {
        //The Client object to track the current login user
        BusinessLayer.Client m_Client;

        public URLController()
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] == null)
                System.Web.HttpContext.Current.Session["CurrentUser"] = new Nintex.BusinessLayer.Client();
            m_Client = (BusinessLayer.Client)System.Web.HttpContext.Current.Session["CurrentUser"];
        }

        /// <summary>
        /// Action to show the Clients URL
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            m_Client.GetURLs();
            return View(m_Client.URLs);
        }

        // GET: URL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: URL/Create
        /// <summary>
        /// Creates the URL and generates the short URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Nintex.BusinessLayer.URL url)
        {
            try
            {
                url.Clients.Add(m_Client);
                url.Add();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: URL/Delete/5
        /// <summary>
        /// Action to delete the URL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            BusinessLayer.URL url = new BusinessLayer.URL { Id = id };
            url.Delete();

            m_Client.GetURLs();
            return RedirectToAction("Index");
        }

        // POST: URL/Delete/5
        /// <summary>
        /// After deleting the URL, this Action redirects to Index
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
