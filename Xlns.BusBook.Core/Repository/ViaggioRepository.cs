using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;

namespace Xlns.BusBook.Core.Repository
{
    public class ViaggioRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Viaggio> GetViaggi()
        {
            return getAll<Viaggio>();
        }

        public Viaggio GetById(int id)
        {
            return base.getDomainObjectById<Viaggio>(id);
        }

        public Tappa GetTappaById(int id)
        {
            return base.getDomainObjectById<Tappa>(id);
        }

        public void Save(Viaggio viaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Viaggio>(viaggio);
                    om.CommitOperation();
                    logger.Info("Dati del viaggio {0} salvati con successo", viaggio.Id);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio del viaggio";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void Save(Tappa tappa)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Tappa>(tappa);
                    om.CommitOperation();
                    logger.Info("Dati della tappa {0} salvati con successo", tappa.Id);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio della tappa";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void deleteTappa(Tappa tappa)
        {
            try
            {
                base.delete<Tappa>(tappa);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante la cancellazione della tappa con id={0}", tappa.Id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }
    }
}
