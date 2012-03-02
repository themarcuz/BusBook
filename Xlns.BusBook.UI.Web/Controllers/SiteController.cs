using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class SiteController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        FlyerRepository flyerRepo = new FlyerRepository();
        AgenziaRepository agenziaRepo = new AgenziaRepository();

        public ActionResult Index(int id)
        {
            var model = new ListFlyerView() { agenzia = agenziaRepo.GetById(id), flyers = flyerRepo.GetFlyersPerAgenzia(id) };
            return View(model);
        }

    }
}
