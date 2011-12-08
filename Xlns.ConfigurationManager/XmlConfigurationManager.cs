using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Xlns.ConfigurationManager
{
    public abstract class XmlConfigurationManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public XElement xml { get; set; }

        /// <summary>
        /// Metodo per restituire un parametro letto da un percorso xml specifico, individuato da TAG1.TAG2...TAGn
        /// <example>consed.pluginfolder</example>
        /// oppure
        /// <example>plugins.centera.connectionstring</example>
        /// </summary>
        /// <returns></returns>
        public string getParameter(string parameterPath)
        {
            try
            {
                var pathElement = parameterPath.Split('.').ToList<string>();
                var res = xml;
                foreach (var elem in pathElement)
                {
                    res = res.Element(elem);
                }
                logger.Debug("Elemento configurazione recuperato");
                logger.Debug(parameterPath + " = " + res.Value);
                return res.Value;
            }
            catch (Exception e)
            {
                logger.ErrorException("Impossibile recuperare l'elemento " + parameterPath, e);
                return null;
            }
        }

        /// <summary>
        /// metodo che recupera una lista di coppie chiave valore, da un percorso xml specificato
        /// </summary>
        /// <param name="nodoListaPath">path del nodo che contiene gli item della lista es. plugins.signature.credentials</param>
        /// <param name="nodoItem">nome del nodo che contiene i singoli item es. credential</param>
        /// <param name="tagKey">nome del nodo che contiene la chiave es. alias</param>
        /// <param name="tagValue">nome del nodo che contiene il valore es value</param>
        /// <returns></returns>
        public Dictionary<string, string> getParameterList(string nodoListaPath, string nodoItem, string tagKey, string tagValue)
        {

            try
            {
                Dictionary<string, string> dicParametri = new Dictionary<string, string>();

                var ListaNodeList = getParameterNode(nodoListaPath).Elements(nodoItem);
                var dictionaryEntryList = from n in ListaNodeList select new { key = n.Element(tagKey).Value, value = n.Element(tagValue).Value };

                foreach (var dictionaryEntry in dictionaryEntryList)
                {
                    dicParametri.Add(dictionaryEntry.key, dictionaryEntry.value);
                }

                return dicParametri;
            }
            catch (Exception ex)
            {
                logger.ErrorException(string.Format("Impossibile recuperare la lista di elementi [{0}] in [{1}]", nodoItem, nodoListaPath), ex);
                return null;
            }
        }

        /// <summary>
        /// recupera il nodo che contiene un parametro specifico
        /// </summary>
        /// <param name="parameterPath">path del nodo es. plugins.signature.credentials</param>
        /// <returns></returns>
        private XElement getParameterNode(string parameterPath)
        {
            try
            {
                var pathElement = parameterPath.Split('.').ToList<string>();
                var res = xml;
                foreach (var elem in pathElement)
                {
                    res = res.Element(elem);
                }
                logger.Debug("Elemento configurazione recuperato");
                return res;
            }
            catch (Exception e)
            {
                logger.ErrorException("Impossibile recuperare l'elemento " + parameterPath, e);
                throw;
            }
        }
    }

}
