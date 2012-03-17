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
        AgenziaRepository agenziaRepo = new AgenziaRepository();

        int limitResults = 5; //TODO: metterlo in configurazione

        public ActionResult ListPartial(int idAgenzia)
        {
            var model = new ListFlyerView() { agenzia = agenziaRepo.GetById(idAgenzia), flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia, limitResults) };
            return PartialView(model);
        }

        public ActionResult Edit(int id)
        {
            var flyer = setFlyerInEdit(id);
            var flyerEdit = new FlyerEditView(flyer);
            flyerEdit.RedirectOnSave = Request.UrlReferrer.OriginalString;

            return View(flyerEdit);
        }

        public ActionResult Detail(int id)
        {
            return View(flyerRepo.GetById(id));
        }

        public ActionResult List(int idAgenzia)
        {
            var model = new ListFlyerView() { agenzia = agenziaRepo.GetById(idAgenzia), flyers = flyerRepo.GetFlyersPerAgenzia(idAgenzia) };
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
                return Redirect(flyerEdit.RedirectOnSave);
       
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
            var viaggiPubblicati = viaggiRepo.GetViaggi().ToList();

            var viaggiSelezionabili = FlyerHelper.getViaggiSelezionabili(getFlyerInEdit() ,viaggiPubblicati);

            return PartialView(viaggiSelezionabili);
        }

        public ActionResult ShowSelected(int id)
        {
            return ShowSelectedViaggi(id, false);
        }


        public ActionResult ShowSelectedExternal(int id)
        {
            return ShowSelectedViaggi(id, true);
        }

        private ActionResult ShowSelectedViaggi(int id, bool isDetailExternal)
        {
            var flyer = flyerRepo.GetById(id);

            List<ViaggioSelectView> viaggiSelezionati = new List<ViaggioSelectView>();

            foreach (var viaggioSel in flyer.Viaggi)
            {
                ViaggioSelectView viaggioSelezionato = new ViaggioSelectView() { viaggio = viaggioSel, isSelected = true, isSelectable = false, idFlyer = flyer.Id, isDetailExternal = isDetailExternal };
                viaggiSelezionati.Add(viaggioSelezionato);
            }
            return PartialView("Select", viaggiSelezionati);
        }

        public ActionResult ShowTile(Flyer flyer, bool isShort, bool isEditable, bool isDetailAjax)
        {
            return PartialView(new FlyerTiled() { flyer = flyer, isShort = isShort, isEditable = isEditable, isDetailAjax = isDetailAjax });
        }

        public ActionResult ShowTileAjax()
        {
            var topFlyers = flyerRepo.GetFlyersPerAgenzia(Session.getLoggedAgenzia().Id, limitResults);
            if (topFlyers.Count >= limitResults)
                return PartialView("ShowTile", new FlyerTiled() { flyer = topFlyers[topFlyers.Count - 1], isShort = true, isEditable = true, isDetailAjax = false});
            else
                return new EmptyResult();
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

       public JsonResult CheckViaggiSelected(int hasViaggiSelected)
        {
            var flyer = getFlyerInEdit();

            bool valid = (flyer.Viaggi.Count > 0);

            return Json(valid, JsonRequestBehavior.AllowGet);
        }

       public ActionResult SearchViaggi(ViaggioSearch param)
       {
           //TODO: solo viaggi pubblicati!
           //var viaggiPubblicati = vr.GetViaggi().Where(v => v.DataPubblicazione != null).ToList();
           var viaggiFinded = viaggiRepo.GetViaggi().Where(v => v.Nome.ToUpper().StartsWith(param.searchString.ToUpper())).ToList();

           var viaggiSelezionabili = FlyerHelper.getViaggiSelezionabili(getFlyerInEdit(), viaggiFinded);

           return PartialView("Select", viaggiSelezionabili);
       }


    }
}
