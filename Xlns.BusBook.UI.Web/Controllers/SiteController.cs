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
            return View("Index",model);
        }

        public ActionResult IndexFromFlyer(int id)
        {
            ViewBag.DefaultFlyerId = id;
            var flyer = flyerRepo.GetById(id);
            return Index(flyer.Agenzia.Id);
        }

        public ActionResult DetailViaggio(int id, int idFlyer)
        {
            var vr = new ViaggioRepository();
            var viaggio = vr.GetById(id);

            var detailViaggio = new DetailViaggio() { viaggio = viaggio, idFlyer = idFlyer };

            return View(detailViaggio);
        }

    }
}
