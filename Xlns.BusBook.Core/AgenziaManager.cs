using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;

namespace Xlns.BusBook.Core
{
    public class AgenziaManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public int CalcolaKmViaggiOrganizzati(Agenzia agenzia)
        {            
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var km = session.Query<Viaggio>()
                                .Where(v => v.Agenzia.Id == agenzia.Id)
                                .Sum(v => (int?) v.DistanzaPercorsa) ?? 0;
                    om.CommitOperation();
                    return km;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile calcolare i km di viaggio proposti dall'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public int CalcolaKmViaggiPartecipati(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var km = session.Query<Partecipazione>()
                                .Where(p => p.Utente.Agenzia.Id == agenzia.Id)                                
                                .Sum(p => (int?)p.Viaggio.DistanzaPercorsa) ?? 0;
                    om.CommitOperation();
                    return km;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile calcolare i km di viaggio a cui ha partecipato l'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public int CalcolaNumeroViaggiOrganizzati(Agenzia agenzia) 
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var num = session.Query<Viaggio>()
                                .Count(v => v.Agenzia.Id == agenzia.Id);                              
                    om.CommitOperation();
                    return num;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile calcolare il numero di viaggi proposti dall'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public int CalcolaNumeroViaggiPartecipati(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var num = session.Query<Partecipazione>()
                                .Count(p => p.Utente.Agenzia.Id == agenzia.Id);                       
                    om.CommitOperation();
                    return num;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile calcolare il numero di viaggi a cui ha partecipato l'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }
    }
}
