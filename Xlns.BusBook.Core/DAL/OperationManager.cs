using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Diagnostics;

namespace Xlns.BusBook.Core.DAL
{
    
    public class OperationManager : IDisposable
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        ITransaction tx = null;
        ISession session = null;
        bool isInternalTransaction = false;

        public ISession BeginOperation()
        {
            try
            {
                session = PersistenceManager.Istance.GetSession();
                if (session.Transaction.IsActive)
                {
                    isInternalTransaction = false;
                    tx = session.Transaction;
                    logger.Debug(GetCallerClassDotMethod() + " si è agganciato alla transazione " + tx.GetHashCode());
                }
                else
                {
                    isInternalTransaction = true;
                    tx = session.Transaction;
                    tx.Begin();
                    logger.Debug("Transazione " + tx.GetHashCode() + " creata da " + GetCallerClassDotMethod());
                }
                logger.Debug("La sessione è " + session.GetHashCode());
                return session;
            }
            catch (Exception ex)
            {
                string msg = "Errore nell'apertura dell'operazione";
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        private String GetCallerClassDotMethod() {
            // serve ad intercettare il chiamante per loggare chi sta agendo sulla transazione
            var st = new StackTrace();
            var sf = st.GetFrame(2);
            var methodReference = sf.GetMethod().Name;
            var classReference = sf.GetMethod().DeclaringType.FullName;
            return string.Concat(classReference, ".", methodReference);
        }

        public void CommitOperation()
        {
            if (isInternalTransaction)
            {
                try
                {
                    tx.Commit();
                    logger.Debug(GetCallerClassDotMethod() + " ha chiuso con committ la transazione " + tx.GetHashCode());
                }
                catch (Exception ex)
                {
                    string msg = "Errore durante la fase di commit della transazione";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
                
            }
        }

        public void RollbackOperation()
        {
            if (isInternalTransaction)
            {
                try
                {
                    tx.Rollback();
                    logger.Debug(GetCallerClassDotMethod() + " ha chiuso con rollback la transazione " + tx.GetHashCode());
                }
                catch (Exception ex) 
                {
                    logger.Warn("Problema durante il rollback esplicito");                    
                }
            }
        }

        public void Dispose()
        {
            if (isInternalTransaction)
            {
                if (tx != null)
                {
                    tx.Dispose();
                }
                PersistenceManager.Istance.Close();
            }
        }
    }
}
