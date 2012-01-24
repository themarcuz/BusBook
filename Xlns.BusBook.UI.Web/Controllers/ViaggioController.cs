using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ViaggioRepository vr = new ViaggioRepository();
        private ViaggioManager vm = new ViaggioManager();

        public ActionResult List()
        {
            var viaggi = vr.GetViaggi();
            return View(viaggi);
        }

        [ChildActionOnly]
        public ActionResult TappaEdit(Tappa tappa)
        {
            return PartialView(tappa);
        }        

        [ChildActionOnly]
        public ActionResult ViaggioTiledDetail(Viaggio viaggio) {
            return PartialView(viaggio);
        }

        public ActionResult Detail(int id)
        {
            var viaggio = vr.GetById(id);
            return View(viaggio);
        }

        public ActionResult Create() 
        {
            return RedirectToAction("Edit", new { id = 0 });
        }

        public ActionResult Edit(int id)
        {
            Viaggio viaggio = null;
            if (id == 0)
                viaggio = new Viaggio();
            else
                viaggio = vr.GetById(id);
            return View(viaggio);
        }       

        [HttpPost]
        public ActionResult Save(Viaggio viaggio)
        {
            if (ModelState.IsValid)
            {
                Viaggio oldViaggio = vr.GetById(viaggio.Id);
                if (oldViaggio != null) 
                {
                    viaggio.Tappe = oldViaggio.Tappe;
                }

                vr.Save(viaggio);
                return RedirectToAction("List");
            }
            return View(viaggio);
        }

        public ActionResult EditTappeViaggio(int idViaggio) {
            var viaggio = vr.GetById(idViaggio);            
            return PartialView(viaggio);
        }

        public ActionResult CreateTappa(int tipo, int idViaggio) {
            var viaggio = vr.GetById(idViaggio);            
            var nuovaTappa = new Tappa() 
            { 
                Tipo = (TipoTappa)tipo, 
                Viaggio = viaggio,
                Ordinamento = vm.CalcolaOrdinamentoPerNuovaTappa(viaggio)
            };
            return PartialView("EditTappa", nuovaTappa);
        }

        public ActionResult EditTappa(int id) 
        {
            var tappa = vr.GetTappaById(id);
            return PartialView(tappa);
        }        

        [HttpPost]
        public ActionResult SaveTappa(Tappa tappa) 
        {
            if (tappa.Viaggio != null && tappa.Viaggio.Id != 0)
            {
                tappa.Viaggio = vr.GetById(tappa.Viaggio.Id);
            }
            if (!ModelState.IsValid)
            {
                vr.Save(tappa);
                return RedirectToAction("EditTappeViaggio", new { idViaggio = tappa.Viaggio.Id });
            }
            else 
            {
                string msg = "Impossibile salvare la tappa modificata o creata";
                logger.Error(msg);
                throw new Exception(msg);
            }
        }

        public void DeleteTappaAjax(int id)
        {
            try
            {
                var tappa = vr.GetTappaById(id);
                vr.deleteTappa(tappa);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione della tappa con id={0}", id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg);
            }
        }

    }
}
