using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;

namespace Xlns.BusBook.Core.Repository
{
    public class CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected IList<T> getAll<T>() where T : Model.ModelEntity
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var items = session.Query<T>();
                    om.CommitOperation();
                    return items.ToList();
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    logger.ErrorException("Errore nel recupero degli oggetti " + typeof(T).ToString(), ex);
                    return null;
                }
            }
        }

        protected IList<T> getAll<T>(int maximumRows, int startRowIndex) where T : Model.ModelEntity
        {
            logger.Debug("Elementi da recuperare: {0} - Elementi da saltare: {1}", maximumRows, startRowIndex);
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var items = session.Query<T>()
                                .Skip(startRowIndex)
                                .Take(maximumRows)
                                .ToList();
                    om.CommitOperation();
                    return items;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    logger.ErrorException("Errore nel recupero degli oggetti " + typeof(T).ToString() + " alla pagina "
                        + Decimal.ToInt32(Decimal.Ceiling((startRowIndex / maximumRows))), ex);
                    return null;
                }
            }
        }
        
        protected int countEntityOccurrences<T>() where T : Model.ModelEntity
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var result = session.Query<T>().Count();
                    om.CommitOperation();
                    logger.Debug("Numero entità {0} presenti = {1}", typeof(T).ToString(), result);
                    return result;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel conteggio delle entità " + typeof(T).ToString();
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        protected int update<T>(T domainModelObject) where T : Model.ModelEntity
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    session.SaveOrUpdate(domainModelObject);
                    om.CommitOperation();
                    logger.Info("Salvataggio dell'oggetto " + domainModelObject.GetType().ToString()
                        + " con id = " + domainModelObject.Id + " avvenuto con successo");
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    logger.ErrorException("Errore nel salvataggio dell'oggetto " + domainModelObject.GetType().ToString() + " con id = " + domainModelObject.Id, ex);
                    throw;
                }
                return domainModelObject.Id;
            }
        }

        protected void delete<T>(T domainModelObject) where T : Model.ModelEntity
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    session.Delete(domainModelObject);                    
                    logger.Info("Eliminazione dell'oggetto " + domainModelObject.GetType().ToString()
                        + " con id = " + domainModelObject.Id + " avvenuta con successo");
                    om.CommitOperation();
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = string.Format("Impossibile eliminare l'oggetto {0} con id={1}",
                        domainModelObject.GetType().ToString(), domainModelObject.Id);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        protected T getDomainObjectById<T>(int Id) where T : Model.ModelEntity, new()
        {
            if (Id == 0) return new T();
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var entity = (from o in session.Query<T>()
                                  where o.Id == Id
                                  select o).Single();
                    om.CommitOperation();
                    return entity;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Impossibile recuperare l'oggetto " + typeof(T).ToString() + " con id = " + Id;
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }
    }
}
