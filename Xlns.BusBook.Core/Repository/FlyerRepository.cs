using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;

namespace Xlns.BusBook.Core.Repository
{
    public class FlyerRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Flyer> GetFlyers()
        {
            return getAll<Flyer>();
        }

        public IList<Flyer> GetFlyersPerAgenzia(int idAgenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var flyers = session.Query<Flyer>().Where(p => p.Agenzia.Id == idAgenzia).OrderByDescending(p => p.Id).ToList();                    
                    om.CommitOperation();
                    logger.Debug("Per l'agenzia {0} sono state trovate {1} flyers", idAgenzia, flyers.Count);
                    return flyers;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare i flyers per l'agenzia {0}", idAgenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IList<Flyer> GetFlyersPerAgenzia(int idAgenzia, int limtResults)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var flyers = session.Query<Flyer>().Where(p => p.Agenzia.Id == idAgenzia).OrderByDescending(p => p.Id).Take(limtResults).ToList();
                    om.CommitOperation();
                    logger.Debug("Per l'agenzia {0} sono state trovate {1} flyers", idAgenzia, flyers.Count);
                    return flyers;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare i flyers per l'agenzia {0}", idAgenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public Flyer GetById(int id)
        {
            return base.getDomainObjectById<Flyer>(id);
        }

        public void Save(Flyer flyer)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Flyer>(flyer);
                    om.CommitOperation();
                    logger.Info("Dati del flyer {0} salvati con successo", flyer.Id);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio del flyer";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void Delete(Flyer flyer)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    manager.BeginOperation();
                    base.delete<Flyer>(flyer);
                    manager.CommitOperation();
                    logger.Info("Flyer {0} eliminato con successo", flyer.Id);
                }
                catch (Exception ex)
                {
                    string message = "Errore nella cancellazione del flyer";
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        public void Delete(int id)
        {
            var flyer = base.getDomainObjectById<Flyer>(id);
            Delete(flyer);
        }
    }
}
