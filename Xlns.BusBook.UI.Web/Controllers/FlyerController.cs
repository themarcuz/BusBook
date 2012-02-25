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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        FlyerRepository flyerRepo = new FlyerRepository();
        ViaggioRepository viaggiRepo = new ViaggioRepository();

        public ActionResult ListPartial(int idAgenzia, int limitResults)
        {
            var model = new ListFlyerView() { idAgenzia = idAgenzia, flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia, limitResults) };
            return PartialView(model);
        }

        public ActionResult Edit(int id)
        {
            var flyer = setFlyerInEdit(id);

            return View(new FlyerEditView(flyer));
        }

        public ActionResult Detail(int id)
        {
            return View(flyerRepo.GetById(id));
        }

        public ActionResult List(int idAgenzia)
        {
            var model = new ListFlyerView() { idAgenzia = idAgenzia, flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia) };
            return View(model);
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

        private Flyer setFlyerInEdit(int idFlyer)
        {
            Flyer flyer = null;
            if (idFlyer == 0) // nuovo flyer
                flyer = new Flyer() { Agenzia = Session.getLoggedAgenzia(), Viaggi = new List<Viaggio>() };
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

                ViaggioSelectView viaggioSelezionabile = new ViaggioSelectView() { viaggio = viaggioPub, isSelected = selected, isSelectable = true,  idFlyer = flyer.Id };
                viaggiSelezionabili.Add(viaggioSelezionabile);
            }
            return PartialView(viaggiSelezionabili);
        }

        public ActionResult ShowSelected(int id)
        {
            var flyer = flyerRepo.GetById(id);

            List<ViaggioSelectView> viaggiSelezionati = new List<ViaggioSelectView>();

            foreach (var viaggioSel in flyer.Viaggi)
            {
                ViaggioSelectView viaggioSelezionato = new ViaggioSelectView() { viaggio = viaggioSel, isSelected = true, isSelectable = false, idFlyer = flyer.Id };
                viaggiSelezionati.Add(viaggioSelezionato);
            }
            return PartialView("Select",viaggiSelezionati);
        }

        public ActionResult ShowTile(Flyer flyer, bool isShort)
        {
            ViewBag.isShort = isShort;
            return PartialView(flyer);
        }

        public void DeleteAjax(int id)
        {
            try
            {
                flyerRepo.Delete(id);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione del flyer con id={0}", id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg);
            }
        }
    }
}
