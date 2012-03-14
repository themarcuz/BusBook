﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using Xlns.BusBook.ConfigurationManager;

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
            base.update<Viaggio>(viaggio);
        }
        

        public void Save(Tappa tappa)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    om.BeginOperation();
                    var destinazione = tappa.Viaggio.Tappe.Where(t => t.Tipo == TipoTappa.DESTINAZIONE).SingleOrDefault();
                    if (destinazione != null)
                    {
                        logger.Debug("L'ordinamento della destinazione verrà incrementato di 1 per fare posto alla nuova tappa");
                        destinazione.Ordinamento = tappa.Ordinamento + 1;
                        base.update<Tappa>(destinazione);
                    }
                    base.update<Tappa>(tappa);
                    om.CommitOperation();
                    logger.Info("Dati della tappa {0} salvati con successo", tappa);
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
                string msg = String.Format("Errore durante la cancellazione della tappa {0}", tappa);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        public void deleteAllegato(AllegatoViaggio allegato)
        {
            try
            {
                // questo serve altrimenti cerca di cancellare anche il viaggio
                allegato.Viaggio = null;

                base.delete<AllegatoViaggio>(allegato);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante la cancellazione dell'allegato ", allegato);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }
    }
}
