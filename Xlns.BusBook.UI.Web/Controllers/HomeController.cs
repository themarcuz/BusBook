using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.UI.Web.Controllers.Filters;
using Xlns.BusBook.UI.Web.Controllers.Helper;

namespace Xlns.BusBook.UI.Web.Controllers
{    
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        
        public ActionResult Index()
        {
            if (AuthenticationHelper.isLogged(Session))
            {
                return View("DashBoard", new DashBoardInfo(Session.getLoggedUtente()));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            if (AuthenticationHelper.isLogged(Session))
                return RedirectToAction("Index");
            else
                return View(new DashBoardInfo(Session.getLoggedUtente()));
        }
    }
}
