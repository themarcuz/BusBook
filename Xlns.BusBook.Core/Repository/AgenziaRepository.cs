﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;


namespace Xlns.BusBook.Core.Repository
{
    public class AgenziaRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Agenzia> GetAllAgenzie(int maximumRows, int startRowIndex)
        {
            return base.getAll<Agenzia>(maximumRows, startRowIndex);
        }

        public int CountAgenzia()
        {
            var result = base.countEntityOccurrences<Agenzia>();
            logger.Debug("Numero di agenzie totali: {0}", result);
            return result;
        }


        public IList<Agenzia> GetAllAgenzie()
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = base.getAll<Agenzia>().ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore durante il recupero delle agenzie";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }


        public void Save(Agenzia agenzia)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Agenzia>(agenzia);
                    om.CommitOperation();
                    logger.Info("Dati dell'agenzia {0} salvati con successo", agenzia);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio dell'agenzia";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IList<Agenzia> GetAllAgenzie(string q, string ini)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = session.Query<Agenzia>()
                                    .Where(a => a.Nome.ToLower().StartsWith(ini) && a.Nome.ToLower().Contains(q.ToLower()))
                                    .OrderBy(a => a.Nome)
                                    .ToList();
                    om.CommitOperation();
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare l'elenco delle agenzie con iniziare '{0}' e contenenti '{1}'",
                        ini, q);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public Agenzia GetById(int id)
        {
            return base.getDomainObjectById<Agenzia>(id);
        }

        public void Delete(int id)
        {
            var agenzia = base.getDomainObjectById<Agenzia>(id);
            base.delete<Agenzia>(agenzia);
        }

        public Agenzia GetAgenziaByIdUtente(int id)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var agenzia = session.Load<Utente>(id).Agenzia;
                    om.CommitOperation();
                    return agenzia;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare l'agenzia relativa all'utente {0}", id);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }
    }
}
