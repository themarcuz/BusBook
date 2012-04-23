using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xlns.BusBook.UI.Web.Models;

namespace Xlns.BusBook.UI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*** Non più utilizzato, serve per memoria ed esempio ***/
            /*
            routes.MapRoute(
                "Agenzia", // Route name
                "Agenzia/{action}/{id}", // URL with parameters
                new { controller = "Agenzia", action = "List", id = UrlParameter.Optional } // Parameter defaults
            );
            */
            /********************************************************/

            routes.MapRoute(
               "blank", // Route name
               "", // URL with parameters
               new { controller = "Home", action = "Index" } // Parameter defaults
           );

            routes.MapRoute(
                "Home", // Route name
                "Home/{action}", // URL with parameters
                new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "DettaglioViaggio", // Route name
                "Viaggio/Detail/{id}/{from}/{idFlyer}", // URL with parameters
                new { controller= "Viaggio", action = "Detail", 
                    id = UrlParameter.Optional, from = UrlParameter.Optional, idFlyer = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { action = "List", id = UrlParameter.Optional } // Parameter defaults
            );
            

        }

        protected void Session_Start() {
            Session.setItemsPerPage(ConfigurationManager.Configurator.Istance.itemsPerPage);            
        }
        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigurationManager.Configurator.configFileName = AppDomain.CurrentDomain.BaseDirectory + @"Config\BusBook.config";

            log4net.Config.XmlConfigurator.Configure();            
        }
    }
}