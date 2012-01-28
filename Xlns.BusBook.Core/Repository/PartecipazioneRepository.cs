using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;

namespace Xlns.BusBook.Core.Repository
{
    class PartecipazioneRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Partecipazione> GetPartecipazioni()
        {
            return getAll<Partecipazione>();
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
