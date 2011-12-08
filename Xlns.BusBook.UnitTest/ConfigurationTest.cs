using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using NHibernate.Cfg;
using Xlns.BusBook.Core.DAL;

namespace Xlns.BusBook.UnitTest
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void se_non_ho_inizializzato_percorso_del_file_di_configurazione_non_riesco_a_recuperare_oggetto_configuratore()
        {
            try
            {
                var current = ConfigurationManager.Configurator.Istance.xml;
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void inizializzando_percorso_del_file_di_configurazione_oggetto_configuratore_ha_struttura_caricata()
        {
            ConfigurationManager.Configurator.configFileName = @"C:\Xlns\BuX\BusBook\Xlns.BusBook.UI.Web\Config\BusBook.config";
            var current = ConfigurationManager.Configurator.Istance.xml;
            Assert.IsNotNull(ConfigurationManager.Configurator.Istance.xml);
        }

        [TestMethod]
        public void inizializzando_in_modo_errato_percorso_del_file_di_configurazione_non_riesco_a_recuperare_oggetto_configuratore()
        {
            try
            {
                ConfigurationManager.Configurator.configFileName = @"percorso no valido";
                var current = ConfigurationManager.Configurator.Istance.xml;
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void inizializzando_percorso_del_file_di_configurazione_recupero_valore_hibernateConfiguration()
        {
            string expected = "pippo";
            ConfigurationManager.Configurator.configFileName = @"C:\Xlns\BuX\BusBook\Xlns.BusBook.UI.Web\Config\BusBook.config";
            ConfigurationManager.Configurator.Istance.xml = 
                new XElement("configuration", 
                                                new XElement("core",
                                                    new XElement("DAL", 
                                                        new XElement("hibernateConfiguration", "pippo")
                                                    )
                                                 )
                             );
            string actual;
            actual = ConfigurationManager.Configurator.Istance.hibernateConfiguration;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void inizializzando_percorso_del_file_di_configurazione_recupero_valore_connectionString()
        {
            string expected = "pippo";
            ConfigurationManager.Configurator.configFileName = @"C:\Xlns\BuX\BusBook\Xlns.BusBook.UI.Web\Config\BusBook.config";
            ConfigurationManager.Configurator.Istance.xml =
                new XElement("configuration",
                                                new XElement("core",
                                                    new XElement("DAL",
                                                        new XElement("connectionString", "pippo")
                                                    )
                                                 )
                             );
            string actual;
            actual = ConfigurationManager.Configurator.Istance.connectionString;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void inizializzando_percorso_del_file_di_configurazione_recupero_valore_itemsPerPage()
        {
            int expected = 5;
            ConfigurationManager.Configurator.configFileName = @"C:\Xlns\BuX\BusBook\Xlns.BusBook.UI.Web\Config\BusBook.config";
            ConfigurationManager.Configurator.Istance.xml =
                new XElement("configuration",
                                                new XElement("UI",
                                                    new XElement("Web",
                                                        new XElement("itemsPerPage", "5")
                                                    )
                                                 )
                             );
            int actual;
            actual = ConfigurationManager.Configurator.Istance.itemsPerPage;
            Assert.AreEqual(expected, actual);
        }
    }
}
