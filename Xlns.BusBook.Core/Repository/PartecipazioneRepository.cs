using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;

namespace Xlns.BusBook.Core.Repository
{
    public class PartecipazioneRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Partecipazione> GetPartecipazioniAlViaggio(int idViaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var partecipazioni = session.Query<Partecipazione>()
                                         .Where(p => p.Viaggio.Id == idViaggio)
                                         .ToList();                    
                    om.CommitOperation();
                    logger.Debug("Per il viaggio {0} sono state trovate {1} partecipazioni", idViaggio, partecipazioni.Count);
                    return partecipazioni;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile verificare le partecipazioni per il viaggio {0}", idViaggio);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public Partecipazione GetById(int id)
        {
            return base.getDomainObjectById<Partecipazione>(id);
        }

        public void Save(Partecipazione partecipazione)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Partecipazione>(partecipazione);
                    om.CommitOperation();
                    logger.Info("Dati della partecipazione {0} salvati con successo", partecipazione.Id);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio della partecipazione";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public bool HasParticipated(int idUtente, int idViaggio)
        {
            var res = false;
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var partecipazioni = session.Query<Partecipazione>().Where(p => p.Viaggio.Id == idViaggio).Where(v => v.Utente.Id == idUtente).ToList();
                    om.CommitOperation();
                    logger.Debug("Per il viaggio {0} e l'agenzia {1} sono state trovate {2} partecipazioni", idViaggio, idUtente, partecipazioni.Count);
                    res = partecipazioni.Count > 0 ? true : false;
                    return res;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile verificare le partecipazioni per il viaggio {0} e l'agenzia {1}", idViaggio, idUtente);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void DeletePartecipazione(Partecipazione partecipazione)
        {
            try
            {
                base.delete<Partecipazione>(partecipazione);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante la cancellazione della partecipazione {0} relativa all'utente {1}", partecipazione.Id, partecipazione.Utente);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        public Partecipazione GetPartecipazioneUtente(int idUtente, int idViaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var partecipazione = session.Query<Partecipazione>()
                                         .Where(p => p.Viaggio.Id == idViaggio)
                                         .Where(v => v.Utente.Id == idUtente)
                                         .SingleOrDefault();
                    om.CommitOperation();
                    return partecipazione;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile verificare la partecipazione per il viaggio {0} e utente {1}", idViaggio, idUtente);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }
    }
}
