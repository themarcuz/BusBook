using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core;
using Xlns.BusBook.UI.Web.Models;

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

        public ActionResult ListPartial()
        {
            var viaggi = vr.GetViaggi();
            ViewBag.IsFullPage = false;
            return PartialView("List",viaggi);
        }

        [ChildActionOnly]
        public ActionResult TappaEdit(Tappa tappa)
        {
            return PartialView(tappa);
        }

        [ChildActionOnly]
        public ActionResult ViaggioTiledDetail(Viaggio viaggio)
        {
            return PartialView(viaggio);
        }

        public ActionResult Detail(int id)
        {
            var viaggio = vr.GetById(id);
            var loggedUser = Session.getLoggedUtente();
            var pr = new PartecipazioneRepository();
            var hasPartecipated = pr.HasParticipated(loggedUser.Id, id);
            ViewBag.HasPartecipated = hasPartecipated;
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
                viaggio = vm.CreaNuovoViaggio();
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
                viaggio.Agenzia = Session.getLoggedAgenzia();
                vr.Save(viaggio);
                return RedirectToAction("Detail", new { id = viaggio.Id });
            }
            return RedirectToAction("Edit", new { id = viaggio.Id });
        }

        public ActionResult EditTappeViaggio(int idViaggio)
        {
            var viaggio = vr.GetById(idViaggio);
            return PartialView(viaggio);
        }

        public ActionResult CreateTappa(int tipo, int idViaggio)
        {
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

        [HttpPost]
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

        [HttpPost]
        public ActionResult RichiestaPartecipazione(int idViaggio)
        {
            var loggedUser = Session.getLoggedUtente();
            Agenzia agenzia = null;
            if (loggedUser != null)
            {
                var viaggio = vr.GetById(idViaggio);
                //registro che questo utente ha visualizzato i dati                
                vm.RegistraPartecipazione(viaggio, loggedUser);
                agenzia = viaggio.Agenzia;
            }
            return PartialView("RichiestaPartecipazione", agenzia);
        }

        [HttpPost]
        public ActionResult RimuoviPartecipazione(int idViaggio)
        {
            var loggedUser = Session.getLoggedUtente();
            Agenzia agenzia = null;
            if (loggedUser != null)
            {
                var viaggio = vr.GetById(idViaggio);
                var pr = new PartecipazioneRepository();
                var partecipazione = pr.GetPartecipazioneUtente(loggedUser.Id, idViaggio);
                if (partecipazione != null)
                    pr.DeletePartecipazione(partecipazione);
                agenzia = viaggio.Agenzia;
            }
            return PartialView("RichiestaPartecipazione", agenzia);
        }

        [HttpPost]
        public ActionResult Pubblica(int idViaggio)
        {
            System.Threading.Thread.Sleep(2000);
            var viaggio = vr.GetById(idViaggio);
            if (Session.getLoggedAgenzia() != null && viaggio.Agenzia.Id == Session.getLoggedAgenzia().Id)
            {
                var vm = new ViaggioManager();
                try
                {
                    vm.Pubblica(viaggio);
                    return null;
                }
                catch (NonPubblicabileException ex)
                {
                    return new HttpStatusCodeResult(403, ex.Message);
                }
            }
            else
            {
                string msg = "Impossibile pubblicare un viaggio di un'azienda che non sia la propria";
                logger.Warn(msg);
                return new HttpStatusCodeResult(403, msg);
            }

        }

        [ChildActionOnly]
        public ActionResult ListaPartecipanti(int idViaggio) 
        {
            var pr = new PartecipazioneRepository();
            var partecipazioni = pr.GetPartecipazioniAlViaggio(idViaggio);
            return PartialView(partecipazioni);
        }
    }
}
