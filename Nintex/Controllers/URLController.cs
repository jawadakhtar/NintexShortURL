using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nintex.Controllers
{
    public class URLController : Controller
    {
        BusinessLayer.Client m_Client;

        public URLController()
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] == null)
                System.Web.HttpContext.Current.Session["CurrentUser"] = new Nintex.BusinessLayer.Client();
            m_Client = (BusinessLayer.Client)System.Web.HttpContext.Current.Session["CurrentUser"];
        }

        // GET: URL
        public ActionResult Index()
        {
            m_Client.GetURLs();
            return View(m_Client.URLs);
        }

        // GET: URL/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: URL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: URL/Create
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

        // GET: URL/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: URL/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: URL/Delete/5
        public ActionResult Delete(int id)
        {
            BusinessLayer.URL url = new BusinessLayer.URL { Id = id };
            url.Delete();

            m_Client.GetURLs();
            return RedirectToAction("Index");
        }

        // POST: URL/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
