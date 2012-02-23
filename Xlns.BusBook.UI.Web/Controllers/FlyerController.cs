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
        ViaggioRepository viaggiRepo = new ViaggioRepository();

        public ActionResult ListPartial(int idAgenzia)
        {
            var model = new ListFlyerView() { idAgenzia = idAgenzia, flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia) };
            return PartialView(model);
        }

        public ActionResult Edit(int idFlyer, int idAgenzia)
        {
            var flyer = setFlyerInEdit(idFlyer, idAgenzia);

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
                Flyer flyer = getFlyerInEdit();

                //queste sono le info del flyer che posso modificare dalla pagina di edit
                flyer.Descrizione = flyerEdit.Descrizione;
                flyer.Titolo = flyerEdit.Titolo;

                flyerRepo.Save(flyer);
                return RedirectToAction("DashBoard","Home");
            }
            return View("Edit",flyerEdit);
        }

        [HttpPost]
        public ActionResult ToggleViaggio(int idViaggio)
        {
            var flyer = getFlyerInEdit();

            var viaggio = viaggiRepo.GetById(idViaggio);

            if (flyer.Viaggi.Any(v => v.Id == viaggio.Id))
                flyer.Viaggi.Remove(viaggio);
            else
                flyer.Viaggi.Add(viaggio);

            return null;
            
            }


        private Flyer getFlyerInEdit()
        {
            return Session.getFlyerInModifica();
        }

        private Flyer setFlyerInEdit(int idFlyer, int idAgenzia)
        {
            Agenzia agenzia = agenziaRepo.GetById(idAgenzia);
            Flyer flyer = null;
            if (idFlyer == 0) // nuovo flyer
                flyer = new Flyer() { Agenzia = agenzia, Viaggi = new List<Viaggio>() };
            else //flyer già esistente
                flyer = flyerRepo.GetById(idFlyer);

            Session.setFlyerInModifica(flyer);

            return flyer;
        }

        public ActionResult Select()
        {
            //TODO: solo viaggi pubblicati!
            //var viaggiPubblicati = vr.GetViaggi().Where(v => v.DataPubblicazione != null).ToList();
            var viaggiPubblicati = viaggiRepo.GetViaggi();

            var flyer = getFlyerInEdit();

            List<ViaggioSelectView> viaggiSelezionabili = new List<ViaggioSelectView>();

            foreach (var viaggioPub in viaggiPubblicati)
            {
                bool selected = false;

                if (flyer.Viaggi != null && flyer.Viaggi.Any(v => v.Id == viaggioPub.Id))
                    selected = true;

                ViaggioSelectView viaggioSelezionabile = new ViaggioSelectView() { viaggio = viaggioPub, isSelected = selected, idFlyer = flyer.Id };
                viaggiSelezionabili.Add(viaggioSelezionabile);
            }
            return PartialView(viaggiSelezionabili);
        }
    }
}
