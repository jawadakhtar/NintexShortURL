using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nintex.Controllers
{
    public class HomeController : Controller
    {
        BusinessLayer.Client m_Client;

        public HomeController()
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] == null)
                System.Web.HttpContext.Current.Session["CurrentUser"] = new Nintex.BusinessLayer.Client();
            m_Client = (BusinessLayer.Client)System.Web.HttpContext.Current.Session["CurrentUser"];
        }

        public ActionResult Index()
        {
            if (m_Client.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "URL");
            }
            
            return View(m_Client);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is Nintex Test Application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Jawad Mohsin Akhtar.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string loginId, string password)
        {
            m_Client.LoginId = loginId;
            m_Client.Password = password;
            try
            {
                m_Client.Login();
            }
            catch(Exception ex)
            {
                ///todo: handle the exception and show the appropriate error message 
                return View(m_Client);
            }


            return RedirectToAction("Index", "URL");
        }

        [HttpPost]
        public ActionResult Register(string loginId, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return View(m_Client);
            }

            m_Client.LoginId = loginId;
            m_Client.Password = password;
            try
            {
                m_Client.Register();
            }
            catch (Exception ex)
            {
                ///todo: handle the exception and show the appropriate error message 
            }

            return View(m_Client);
        }

        /// <summary>
        /// Redirect to URL Views
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("Index", "URL");
        }
    }
}