using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.Core
{
    public class ViaggioManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ViaggioRepository vr = new ViaggioRepository();
        private PartecipazioneRepository pr = new PartecipazioneRepository();

        public int CalcolaOrdinamentoPerNuovaTappa(Viaggio viaggio)
        {
            logger.Debug("Calcolo ordiamento nuova tappa per viaggio {0}", viaggio.Id);
            var tappe = viaggio.Tappe.Where(t => t.Tipo == TipoTappa.TAPPA);
            if (tappe != null && tappe.Count() > 0)
            {
                int result = tappe.Max(t => t.Ordinamento) + 1;
                logger.Debug("Tappe precedenti trovate, ordinamento nuova tappa = {0}", result);
                return result;
            }
            else
            {
                logger.Debug("Nessuna tappa trovata, ordinamento nuova tappa = 1");
                return 1;
            }
        }

        public void RegistraPartecipazione(Viaggio viaggio, Utente utenteRichiedente)
        {
            try
            {
                Partecipazione richiestaPartecipazione = new Partecipazione()
                    {
                        Viaggio = viaggio,
                        Utente = utenteRichiedente,
                        DataRichiesta = DateTime.Now
                    };
                pr.Save(richiestaPartecipazione);
                logger.Info("L'azienda {0} ha registrato la sua partecipazione al viaggio {1}", 
                    utenteRichiedente.Agenzia, viaggio);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Impossibile registrare la partecipazione al viaggio {0} da parte dell'agenzia {1}",
                    viaggio, utenteRichiedente.Agenzia);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        public void Pubblica(Viaggio viaggio)
        {
            try
            {
                var partenza = viaggio.Tappe.Where(t => t.Tipo == TipoTappa.PARTENZA).FirstOrDefault();
                var destinazione = viaggio.Tappe.Where(t => t.Tipo == TipoTappa.DESTINAZIONE).FirstOrDefault();
                if (partenza == null || destinazione == null)
                    throw new NonPubblicabileException("Impossibile pubblicare un viaggio senza specificare almeno la partenza e la destinazione");
                viaggio.DataPubblicazione = DateTime.Now;
                vr.Save(viaggio);
                logger.Info("Il viaggio {0} è stato pubblicato", viaggio);
            }
            catch (NonPubblicabileException ex)
            {
                logger.WarnException("Impossibile pubblicare il viaggio", ex);
                throw;
            }
            catch (Exception ex)
            {
                string msg = "Impossibile pubblicare il viaggio " + viaggio;
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }


        public bool IsPubblicato(Viaggio viaggio)
        {
            return viaggio.DataPubblicazione.HasValue;
        }


        public Viaggio CreaNuovoViaggio()
        {
            var viaggio = new Viaggio() 
            {
                DataPartenza = DateTime.Today.AddDays(1),
                DataChiusuraPrenotazioni = DateTime.Today
            };
            return viaggio;
        }
    }
}
