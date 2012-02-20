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
    public class FlyerController : Controller
    {
        FlyerRepository flyerRepo = new FlyerRepository();
        AgenziaRepository agenziaRepo = new AgenziaRepository();


        public ActionResult Index()
        {
            var allFlyers = flyerRepo.GetFlyers();
            return View(allFlyers);
        }

        public ActionResult Test()
        {
            
            var viaggiRepo = new ViaggioRepository();
            var viaggi = viaggiRepo.GetViaggi().Where(v => v.Agenzia.Id == 2).ToList();

            var flyer = new Flyer() { Titolo = "test", Agenzia = Session.getLoggedAgenzia(), Viaggi = viaggi};

            flyerRepo.Save(flyer);

            return View("Index");
        }

        public ActionResult ListPartial(int idAgenzia)
        {
            var model = new ListFlyerView() { idAgenzia = idAgenzia, flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia) };
            return PartialView(model);
        }

        public ActionResult Edit(int idFlyer, int idAgenzia)
        {
            Agenzia agenzia = agenziaRepo.GetById(idAgenzia);
            Flyer flyer = null;
            if (idFlyer == 0)
                flyer = new Flyer() { Agenzia = agenzia };
            else
                flyer = flyerRepo.GetById(idFlyer);

            return View(new FlyerEditView(flyer));
        }

        public ActionResult List(int idAgenzia)
        {
            return View(flyerRepo.GetFlyersPerAgenzia(idAgenzia));
        }

        [HttpPost]
        public ActionResult Save(FlyerEditView flyerEdit)
        {
            if (ModelState.IsValid)
            {
                Agenzia ag = agenziaRepo.GetById(flyerEdit.idAgenzia);

                flyerRepo.Save(new Flyer() { Id = flyerEdit.Id, Agenzia = ag, Descrizione = flyerEdit.Descrizione, Titolo = flyerEdit.Titolo});
                return RedirectToAction("DashBoard","Home");
            }
            return View("Edit",flyerEdit);
        }
    }
}
