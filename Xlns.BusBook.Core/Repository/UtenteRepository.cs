using System;
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

        public Utente GetByUsername(string email)
        {
            using (var manager = new OperationManager())
            {
                try
                {
                    var session = manager.BeginOperation();
                    var res = session.Query<Utente>()
                                    .Where(u => u.Username.ToLower().Equals(email)).SingleOrDefault();
                    manager.CommitOperation();
                    return res;
                }
                catch (Exception ex)
                {
                    manager.RollbackOperation();
                    string message = "Error";
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
        }

        public IList<Utente> GetAllUtenti()
        {
            try
            {
                var res = base.getAll<Utente>();
                return res;
            }
            catch (Exception ex)
            {
                string message = "Errore durante il recupero degli utenti";
                logger.ErrorException(message, ex);
                throw new Exception(message, ex);
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
                    string message = "Error";
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
                    logger.Info("Utente{0} eliminato con successo", utente.Id);
                }
                catch (Exception ex)
                {
                    string message = "Errore nella cancellazione dell'utente";
                    logger.ErrorException(message, ex);
                    throw new Exception(message, ex);
                }
            }
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
                    logger.Info("Dati dell'utente {0} salvati con successo", utente.Id);
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
