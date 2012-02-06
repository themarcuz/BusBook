using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            var loggedUser = Session.getLoggedUtente();

            if (loggedUser == null)
                return RedirectToAction("Index");
            else
                return View(new DashBoardInfo(loggedUser));
        }
    }
}
