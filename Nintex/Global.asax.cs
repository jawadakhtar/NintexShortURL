using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nintex
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            BusinessLayer.Configurator.DatabaseLink.Initialize("NintexUrlDbEntities");
            //var webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            //string server, userid, password, db;
            //BusinessLayer.Configurator.DatabaseLink.Instance().GetDatabaseConnectionParameters(webConfig, out server, out db, out userid, out password);
            //if (server != "localhost" || db != "NintexUrlDb" || userid != "sa" || password != "pak")
            //{
            //    BusinessLayer.Configurator.DatabaseLink.Instance().ConfigureDatabase(webConfig, "localhost", "NintexUrlDb", "sa", "pak");
            //}
        }
    }
}
