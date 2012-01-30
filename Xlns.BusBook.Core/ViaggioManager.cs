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

        public int CalcolaOrdinamentoPerNuovaTappa(Viaggio viaggio) {
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
            Partecipazione richiestaPartecipazione = new Partecipazione() { Viaggio = viaggio, Utente = utenteRichiedente, DataRichiesta = DateTime.Now };
            pr.Save(richiestaPartecipazione);
        }
    }
}
