using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.ConfigurationManager;
using System.Xml.Linq;

namespace Xlns.BusBook.ConfigurationManager
{
    public class Configurator : XmlConfigurationManager
    {
        private static Configurator _istance;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static Object _lock = new Object();
        

        public static Configurator Istance
        {
            get
            {
                lock (_lock)
                {
                    if (_istance == null)
                    {
                        _istance = new Configurator(configFileName);
                        logger.Debug("Creata istanza singleton del ConfigManager");
                    }
                    return _istance;
                }
            }
        }
        
        public static string configFileName { get; set; }

        private Configurator(string configPath)
        {
            try
            {
                configFileName = configPath;
                logger.Debug("Costruzione del ConfigManager: viene cercato il file /n" + configFileName);
                xml = XElement.Load(configFileName);
                logger.Info("File di configurazione caricato");
                logger.Debug(xml.ToString());
            }
            catch (Exception e)
            {
                logger.ErrorException("Errore nella lettura del file di configurazione", e);
                throw;
            }
        }

        public void ReloadConfig()
        {
            xml = XElement.Load(configFileName);
            logger.Info("File di configurazione ricaricato");
            logger.Debug(xml.ToString());
        }

        public string hibernateConfiguration { get { return base.getParameter("core.DAL.hibernateConfiguration"); } }
        public string connectionString { get { return base.getParameter("core.DAL.connectionString"); } }
        public int itemsPerPage { get { return int.Parse(base.getParameter("UI.Web.itemsPerPage")); } }
    }
}
