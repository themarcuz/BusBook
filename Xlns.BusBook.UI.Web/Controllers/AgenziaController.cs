using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.UI.Web.Models.Helper;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class AgenziaController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        AgenziaRepository ar = new AgenziaRepository();

        //
        // GET: /Agenzia/

        public ActionResult List(string q, string ini)
        {
            logger.Debug("Caricamento agenzie con iniziale='{0}' e filtro='{1}'", ini, q);
            if (string.IsNullOrEmpty(q)) q = "";
            if (string.IsNullOrEmpty(ini)) ini = "";
            var myModel = ar.GetAllAgenzie(q, ini);
            var agencies = new AgenzieView(myModel.ToList(), q, ini);            
            return View(agencies);
        }


        public ActionResult Search(string q, string ini)
        {
            logger.Debug("Caricamento agenzie con iniziale='{0}' e filtro='{1}'", ini, q);
            
            // serve in sviluppo per evidenziare l'effetto del loader, altrimenti è troppo veloce e non si vede neanche
            System.Threading.Thread.Sleep(1000);
            
            if (string.IsNullOrEmpty(q)) q = "";
            if (string.IsNullOrEmpty(ini)) ini = "";
            var myModel = ar.GetAllAgenzie(q, ini);
            var agencies = new AgenzieView(myModel.ToList(), q, ini);
            return PartialView(agencies);
        }

        public ActionResult Create()
        {            
            return RedirectToAction("Edit", new { id = 0 });
        }

        [HttpPost]
        public ActionResult Create(Agenzia agenzia) 
        {
            if (ModelState.IsValid)
            {
                ar.Save(agenzia);
                return RedirectToAction("List");
            }
            return View(agenzia);
        }

        public ActionResult Detail(int id) 
        {
            Agenzia agenzia = ar.GetById(id);            
            return View(agenzia);
        }

        public ActionResult Delete(int id) {
            try
            {
                ar.Delete(id);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione dell'agenzia con id={0}", id);
                logger.ErrorException(msg, ex);
                ViewBag.ErrorText = msg;
            }
            return RedirectToAction("List");
        }

        public void DeleteAjax(int id)
        {
            try
            {
                ar.Delete(id);                                
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione dell'agenzia con id={0}", id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg);
            }            
        }

        public ActionResult Edit(int id) 
        {   
            Agenzia agenzia = null;
            if (id == 0)
                agenzia = new Agenzia();
            else
                agenzia = ar.GetById(id);
            return View(agenzia);
        }
       
    }    
}
