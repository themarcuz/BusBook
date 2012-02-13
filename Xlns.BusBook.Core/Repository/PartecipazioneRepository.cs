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

        public IList<Partecipazione> GetPartecipazioni()
        {
            return getAll<Partecipazione>();
        }

        public IList<Partecipazione> GetPartecipazioniAlViaggio(int idViaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var partecipazioni = session.Query<Partecipazione>().Where(p => p.Viaggio.Id == idViaggio).ToList();                    
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
    }
}
