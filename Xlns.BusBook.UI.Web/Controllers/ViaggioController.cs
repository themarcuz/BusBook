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
        
        public ActionResult EditTappa(Tappa tappa)
        {
            return PartialView(tappa);
        }

    }
}
