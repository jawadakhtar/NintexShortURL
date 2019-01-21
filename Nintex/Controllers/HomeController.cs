using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nintex.Controllers
{
    /// <summary>
    /// Home Controller for Client Registration and Login. 
    /// It redirects the user to Url management page
    /// </summary>
    public class HomeController : Controller
    {
        //The Client object to track the current login user
        BusinessLayer.Client m_Client;

        public HomeController()
        {
            if(Nintex.TestContext.IsTesting == true)
            {
                m_Client = BusinessLayer.Client.GetClient("test", "test");
                return;
            }

            if (System.Web.HttpContext.Current.Session["CurrentUser"] == null)
                System.Web.HttpContext.Current.Session["CurrentUser"] = new Nintex.BusinessLayer.Client();
            m_Client = (BusinessLayer.Client)System.Web.HttpContext.Current.Session["CurrentUser"];
        }

        /// <summary>
        /// Action to show the Registration and Login page or redirects the Login user to the URL management
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (m_Client.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "URL");
            }            
            return View(m_Client);
        }

        /// <summary>
        /// Show the About view
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "This is Nintex Test Application.";

            return View();
        }

        /// <summary>
        /// Show the Contact view
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Jawad Mohsin Akhtar.";

            return View();
        }


        /// <summary>
        /// Action to login the User and redirect to URL management
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Action to Register the new user
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
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
        /// Redirect to URL managements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("Index", "URL");
        }
    }
}