﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;
using Xlns.BusBook.Core.Enums;

namespace Xlns.BusBook.Core.Repository
{
    public class MessaggioRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IList<Messaggio> GetMessaggi()
        {
            return getAll<Messaggio>();
        }

        public Messaggio GetById(int id)
        {
            return base.getDomainObjectById<Messaggio>(id);
        }

        public IList<Messaggio> GetMessaggiUnreadByDestinatario(int idDestinatario)
        {
            IList<Messaggio> messaggi = GetMessaggi();
            return messaggi.Where(u => u.Destinatario.Id == idDestinatario).Where(r => r.Stato == (int)MessaggioEnumerator.NonLetto).ToList();
        }

        public IList<Messaggio> GetMessaggiByDestinatario(int idDestinatario)
        {
            IList<Messaggio> messaggi = GetMessaggi();
            return messaggi.Where(u => u.Destinatario.Id == idDestinatario).ToList();
        }

        public void Save(Messaggio messaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    base.update<Messaggio>(messaggio);
                    om.CommitOperation();
                    logger.Info("Dati del messaggio {0} salvati con successo", messaggio.Id);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = "Errore nel salvataggio del messaggio";
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void DeleteMessaggio(Messaggio messaggio)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante la cancellazione del messaggio {0}", messaggio.Id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }
    }
}
