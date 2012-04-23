using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.Core.Mailer;
using Xlns.BusBook.Core.Enums;
using Xlns.BusBook.UI.Web.Controllers.Helper;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ViaggioRepository vr = new ViaggioRepository();
        private ViaggioManager vm = new ViaggioManager();        

        public ActionResult ListPartial()
        {
            var viaggi = vr.GetViaggi();
            ViewBag.IsFullPage = false;            
            return PartialView("List", viaggi);
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

        public ActionResult Detail(int id, string from = null, int idFlyer = 0)
        {
            var viaggio = vr.GetById(id);
            ViewBag.From = from;
            ViewBag.FlyerId = idFlyer;
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
                    viaggio.Depliant = oldViaggio.Depliant;
                    viaggio.PromoImage = oldViaggio.PromoImage;
                }
                viaggio.Agenzia = Session.getLoggedAgenzia();

                // Gestione depliant e immagine promozionale
                if (Request.Files != null)
                {
                    foreach (string fileName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fileName] as HttpPostedFileBase;
                        if (file.ContentLength == 0)
                            continue;
                        if (vm.isValidDepliantMimeType(file.FileName))
                        {
                            logger.Info("Caricamento depliant per il viaggio {0}", viaggio);
                            Int32 length = file.ContentLength;
                            byte[] rawFile = new byte[length];
                            file.InputStream.Read(rawFile, 0, length);
                            var allegato = new AllegatoViaggio()
                            {
                                RawFile = rawFile,
                                NomeFile = file.FileName,
                                Viaggio = viaggio
                            };
                            viaggio.Depliant = allegato;
                        }
                        if (vm.isValidImageMimeType(file.FileName))
                        {
                            logger.Info("Caricamento immagine promozionale per il viaggio {0}", viaggio);
                            Int32 length = file.ContentLength;
                            byte[] rawFile = new byte[length];
                            file.InputStream.Read(rawFile, 0, length);
                            var allegato = new AllegatoViaggio()
                            {
                                RawFile = rawFile,
                                NomeFile = file.FileName,
                                Viaggio = viaggio
                            };
                            viaggio.PromoImage = allegato;
                        }                       
                    }
                }                
                vm.Save(viaggio);
                if (viaggio.Tappe != null && viaggio.Tappe.Count > 1 && viaggio.Tappe.SingleOrDefault(t => t.Tipo == TipoTappa.DESTINAZIONE) != null)
                {
                    logger.Debug("Il percorso del viaggio è stato definito, per cui lo si redirige alla pagina di dettaglio per verifica");
                    return RedirectToAction("Detail", new { id = viaggio.Id });
                }
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
                vm.DeleteTappa(id);                
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
            if (AuthenticationHelper.isLogged(Session))
            {
                var viaggio = vr.GetById(idViaggio);
                //registro che questo utente ha visualizzato i dati                
                vm.RegistraPartecipazione(viaggio, loggedUser);
                var mr = new MessaggioRepository();
                Messaggio messaggio = new Messaggio();
                messaggio.Mittente = loggedUser;
                messaggio.Destinatario = viaggio.Agenzia.Utenti.FirstOrDefault();
                var testoMessaggio = ConfigurationManager.Configurator.Istance.messagesPartecipaMessage
                    .Replace("{agenzia}", loggedUser.Agenzia.Nome)
                    .Replace("{viaggio}", viaggio.Nome)
                    .Replace("{descrizioneViaggio}", viaggio.Descrizione);
                messaggio.Testo = testoMessaggio;
                messaggio.Stato = (int)MessaggioEnumerator.NonLetto;
                messaggio.DataInvio = DateTime.Now;
                mr.Save(messaggio);
                MailHelper mh = new MailHelper();
                //mh.SendMail(viaggio.Agenzia.Email, "");
                agenzia = viaggio.Agenzia;
            }
            return PartialView("RichiestaPartecipazione", agenzia);
        }

        

        [HttpPost]
        public ActionResult RimuoviPartecipazione(int idViaggio)
        {
            var loggedUser = Session.getLoggedUtente();
            Agenzia agenzia = null;
            if (AuthenticationHelper.isLogged(Session))
            {
                var viaggio = vr.GetById(idViaggio);
                var pr = new PartecipazioneRepository();
                var partecipazione = pr.GetPartecipazioneUtente(loggedUser.Id, idViaggio);
                if (partecipazione != null)
                    pr.DeletePartecipazione(partecipazione);
                var mr = new MessaggioRepository();
                Messaggio messaggio = new Messaggio();
                messaggio.Mittente = loggedUser;
                messaggio.Destinatario = viaggio.Agenzia.Utenti.FirstOrDefault();
                var testoMessaggio = ConfigurationManager.Configurator.Istance.messagesRimuoviMessage
                    .Replace("{agenzia}", loggedUser.Agenzia.Nome)
                    .Replace("{viaggio}", viaggio.Nome)
                    .Replace("{descrizioneViaggio}", viaggio.Descrizione);
                messaggio.Testo = testoMessaggio;
                messaggio.Stato = (int)MessaggioEnumerator.NonLetto;
                messaggio.DataInvio = DateTime.Now;
                mr.Save(messaggio);
                MailHelper mh = new MailHelper();
                //mh.SendMail(viaggio.Agenzia.Email, "");
                agenzia = viaggio.Agenzia;
            }
            return PartialView("RichiestaPartecipazione", agenzia);
        }

        [HttpPost]
        public ActionResult Pubblica(int idViaggio)
        {            
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
        [HttpPost]
        public ActionResult ReorderTappe(int[] reorderedIds, int idViaggio)
        {
            var viaggio = vr.GetById(idViaggio);
            int order = 1;
            foreach (var id in reorderedIds)
            {
                viaggio.Tappe.Single(t => t.Id == id).Ordinamento = order;
                order++;
            }
            vr.Save(viaggio);
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult Search(ViaggioSearchView searchParams)
        {
            //TODO: Solo Pubblicati!

            var viaggiFound = vm.Search(ViaggioHelper.getViaggioSearchParams(searchParams, false));

            var viaggiSelezionabili = FlyerHelper.getViaggiSelezionabili(Session.getFlyerInModifica(), viaggiFound);

            return Select(viaggiSelezionabili);
        }

        public ActionResult Search(String idDivToUpdate)
        {
            return PartialView(new ViaggioSearchView() { idDivToUpdate = idDivToUpdate });
        }


        public ActionResult Select(List<ViaggioSelectView> viaggi, string from = null, int idFlyer = 0)
        {
            ViewBag.From = from;
            ViewBag.FlyerId = idFlyer;
            //TODO: Solo Pubblicati!
            if (viaggi == null)
            {
                //con questa ricerca li becco tutti
                List<Viaggio> viaggiFound = vm.Search(new ViaggioSearch() { onlyPubblicati = false });

                viaggi = FlyerHelper.getViaggiSelezionabili(Session.getFlyerInModifica(), viaggiFound);
            }

            return PartialView("Select",viaggi);
        }

        public ActionResult SearchTappa(int tipo)
        {
            var tappaSearch = new Tappa()
            {
                Tipo = (TipoTappa)tipo,  
            };
            return PartialView("SearchTappa", tappaSearch);
        }

        public ActionResult ShowSelected(int idFlyer, bool isDetailExternal)
        {
            return Select(FlyerHelper.getViaggiSelezionati(idFlyer, isDetailExternal), "flyer", idFlyer);
        }
    }
}
