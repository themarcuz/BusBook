﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;

namespace Xlns.BusBook.Core.Repository
{
    public class UtenteRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Utente GetById(int id)
        {
            return base.getDomainObjectById<Utente>(id);
        }

        public Utente GetByUsername(string username)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Utente>()
                                    .Where(u => u.Username.ToLower().Equals(username)).SingleOrDefault();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Impossibile recuperare l'utente con username = {0}", username);
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }        

        public IList<Utente> GetAllUtenti(string q, string ini)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Utente>()
                                    .Where(u => u.Cognome.ToLower().StartsWith(ini) && 
                                        u.Cognome.ToLower().Contains(u.Cognome.ToLower()))
                                    .OrderBy(u => u.Cognome)
                                    .ToList();
                    manager.CommitOperation();
                    return res;
                }   
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = String.Format("Errore nel recupero della lista degli utenti con iniziale '{0}' e contenenti '{1}'",
                        ini, q);
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        public void Delete(Utente utente)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    manager.BeginOperation();
                    base.delete<Utente>(utente);
                    manager.CommitOperation();
                    logger.Info("Utente {0} eliminato con successo", utente);
                }
                catch (Exception ex)
                {
                    string message = "Errore nella cancellazione dell'utente";
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        public void Delete(int id)
        {
            var utente = base.getDomainObjectById<Utente>(id);
            Delete(utente);
        }

        public void Save(Utente utente)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    manager.BeginOperation();
                    base.update<Utente>(utente);
                    manager.CommitOperation();
                    logger.Info("Dati dell'utente {0} salvati con successo", utente);
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = "Errore nel salvataggio dell'utente";
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }
    }
}
