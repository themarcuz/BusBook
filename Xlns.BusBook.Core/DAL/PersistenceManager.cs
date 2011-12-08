using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using Xlns.BusBook.ConfigurationManager;
using NHibernate.Context;

namespace Xlns.BusBook.Core.DAL
{

    public class PersistenceManager : IDisposable
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static PersistenceManager _istance;
        private ISessionFactory _SessionFactory;
        private static Object _lock = new Object();

        public static PersistenceManager Istance
        {
            get
            {
                lock (_lock)
                {
                    if (_istance == null)
                    {
                        _istance = new PersistenceManager();
                        logger.Info("Creata nuova istanza del Persistence Manager");
                    }
                    return _istance;
                }
            }
        }

        private PersistenceManager()
        {
            // Initialize
            Configuration cfg = new Configuration();
            cfg.Configure(Configurator.Istance.hibernateConfiguration);
            cfg.SetProperty("connection.connection_string", Configurator.Istance.connectionString);            

            /* Note: The AddAssembly() method requires that mappings be 
             * contained in hbm.xml files whose BuildAction properties 
             * are set to ‘Embedded Resource’. */

            // Add class mappings to configuration object
            System.Reflection.Assembly thisAssembly = typeof(PersistenceManager).Assembly;
            cfg.AddAssembly(thisAssembly);

            // Create session factory from configuration object
            _SessionFactory = cfg.BuildSessionFactory();
        }



        public void Dispose()
        {
            
            _SessionFactory.Close();
            _SessionFactory.Dispose();
        }


        /// <summary>
        /// Close this Persistence Manager and release all resources (connection pools, etc). It is the responsibility of the application to ensure that there are no open Sessions before calling Close().
        /// </summary>
        public void Close()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_SessionFactory);
            currentSession.Close();
            currentSession.Dispose();            
        }


        public ISession GetSession()
        {
            if (!CurrentSessionContext.HasBind(_SessionFactory))
                CurrentSessionContext.Bind(_SessionFactory.OpenSession());

            return _SessionFactory.GetCurrentSession();            
        }

    }
}

