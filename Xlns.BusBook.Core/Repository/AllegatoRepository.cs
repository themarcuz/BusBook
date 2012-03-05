using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.DAL;
using Xlns.BusBook.ConfigurationManager;

namespace Xlns.BusBook.Core.Repository
{
    public class AllegatoRepository : CommonRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Model.Allegato GetAllegatoWithFileById(int id) 
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var allegato = base.getDomainObjectById<Model.Allegato>(id);
                    HydrateAllegato(allegato);
                    om.CommitOperation();
                    return allegato;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Error {0}", null);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }            
        }

        private void HydrateAllegato(Model.Allegato allegato)
        {            
            allegato.RawFile = System.IO.File.ReadAllBytes(allegato.NomeFile);
        }

        
    }
}
