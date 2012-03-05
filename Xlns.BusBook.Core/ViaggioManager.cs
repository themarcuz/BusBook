using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using NHibernate.Linq;
using Xlns.BusBook.ConfigurationManager;

namespace Xlns.BusBook.Core
{
    public class ViaggioManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ViaggioRepository vr = new ViaggioRepository();
        private PartecipazioneRepository pr = new PartecipazioneRepository();

        public int CalcolaOrdinamentoPerNuovaTappa(Viaggio viaggio)
        {
            logger.Debug("Calcolo ordiamento nuova tappa per viaggio {0}", viaggio.Id);
            var tappe = viaggio.Tappe.Where(t => t.Tipo != TipoTappa.DESTINAZIONE);
            if (tappe != null && tappe.Count() > 0)
            {
                int result = tappe.Max(t => t.Ordinamento) + 1;
                logger.Debug("Tappe precedenti trovate, ordinamento nuova tappa = {0}", result);
                return result;
            }
            else
            {
                logger.Debug("Nessuna tappa trovata, ordinamento nuova tappa = 1");
                return 1;
            }
        }

        public void RegistraPartecipazione(Viaggio viaggio, Utente utenteRichiedente)
        {
            try
            {
                Partecipazione richiestaPartecipazione = new Partecipazione()
                    {
                        Viaggio = viaggio,
                        Utente = utenteRichiedente,
                        DataRichiesta = DateTime.Now
                    };
                pr.Save(richiestaPartecipazione);
                logger.Info("L'azienda {0} ha registrato la sua partecipazione al viaggio {1}",
                    utenteRichiedente.Agenzia, viaggio);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Impossibile registrare la partecipazione al viaggio {0} da parte dell'agenzia {1}",
                    viaggio, utenteRichiedente.Agenzia);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        public void Pubblica(Viaggio viaggio)
        {
            try
            {
                var partenza = viaggio.Tappe.Where(t => t.Tipo == TipoTappa.PARTENZA).FirstOrDefault();
                var destinazione = viaggio.Tappe.Where(t => t.Tipo == TipoTappa.DESTINAZIONE).FirstOrDefault();
                if (partenza == null || destinazione == null)
                    throw new NonPubblicabileException("Impossibile pubblicare un viaggio senza specificare almeno la partenza e la destinazione");
                viaggio.DistanzaPercorsa = CalcolaDistanzaPercorsa(viaggio);
                viaggio.DataPubblicazione = DateTime.Now;
                vr.Save(viaggio);
                logger.Info("Il viaggio {0} è stato pubblicato", viaggio);
            }
            catch (NonPubblicabileException ex)
            {
                logger.WarnException("Impossibile pubblicare il viaggio", ex);
                throw;
            }
            catch (Exception ex)
            {
                string msg = "Impossibile pubblicare il viaggio " + viaggio;
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }

        private int CalcolaDistanzaPercorsa(Viaggio viaggio)
        {
            try
            {
                string origine = viaggio.Tappe
                                    .Where(t => t.Tipo == TipoTappa.PARTENZA)
                                    .Select(t => string.Format("{0},{1}", t.Location.Lat, t.Location.Lng))
                                    .SingleOrDefault();
                string destinazione = viaggio.Tappe
                                    .Where(t => t.Tipo == TipoTappa.DESTINAZIONE)
                                    .Select(t => string.Format("{0},{1}", t.Location.Lat, t.Location.Lng))
                                    .SingleOrDefault();
                var req = new Xlns.Google.Maps.Directions.Request(origine, destinazione);
                req.Waypoints = new List<String>();
                viaggio.Tappe
                        .Where(t => t.Tipo != TipoTappa.DESTINAZIONE && t.Tipo != TipoTappa.PARTENZA)
                        .ForEach(t => req.Waypoints.Add(
                            String.Format("{0},{1}", t.Location.Lat, t.Location.Lng))
                        );

                var svcHelper = new Xlns.Google.Maps.Directions.Services();
                var distanza = svcHelper.CalcolaDistanzaPercorsa(req);                 
                logger.Info("La distanza percorsa per il viaggio {0} è stimata in {1} km", viaggio, distanza);
                return distanza;
            }
            catch (Exception ex)
            {
                string msg = String.Format("Impossibile calcolare la distanza percorsa per il viaggio {0}", viaggio);
                logger.ErrorException(msg, ex);
                throw new Exception(msg, ex);
            }
        }


        public bool IsPubblicato(Viaggio viaggio)
        {
            return viaggio.DataPubblicazione.HasValue;
        }


        public Viaggio CreaNuovoViaggio()
        {
            var viaggio = new Viaggio()
            {
                DataPartenza = DateTime.Today.AddDays(1),
                DataChiusuraPrenotazioni = DateTime.Today
            };
            return viaggio;
        }

        public IList<Viaggio> GetProposteAgenzia(Agenzia agenzia)
        {
            var ar = new AgenziaRepository();
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    logger.Info("Recupero dei viaggi proposti dall'agenzia {0}", agenzia);
                    var viaggi = session.Query<Viaggio>()
                                    .Where(v => v.Agenzia.Id == agenzia.Id)
                                    .ToList();
                    logger.Debug("Viaggi proposti: {0}", viaggi.Count);
                    om.CommitOperation();
                    return viaggi;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare i viaggi proposti dall'agenzia {0}", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public IList<Viaggio> GetPartecipazioniAgenzia(Agenzia agenzia)
        {
            var ar = new AgenziaRepository();
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    logger.Info("Recupero dei viaggi a cui l'agenzia {0} ha partecipato", agenzia);
                    var viaggi = session.Query<Partecipazione>()
                                    .Where(p => p.Utente.Agenzia.Id == agenzia.Id)
                                    .Select(p => p.Viaggio)
                                    .ToList();
                    logger.Debug("Viaggi trovati: {0}", viaggi.Count);
                    om.CommitOperation();
                    return viaggi;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare i viaggi a cui l'agenzia {0} ha partecipato", agenzia);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public Viaggio GetViaggioByDepliant(int idDepliant) {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var viaggio = session.Query<Viaggio>()
                                    .Where(v => v.Depliant.Id == idDepliant)
                                    .Single();
                    om.CommitOperation();
                    logger.Debug("Il depliant {0} si riferisce al viaggio {1}", idDepliant, viaggio);
                    return viaggio;
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile recuperare il viaggio a cui è associato il depliant {0}", idDepliant);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        public void Save(Viaggio viaggio)
        {
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    vr.Save(viaggio);
                    // serve per valorizzare gli ID generati dal DB
                    session.Flush();
                    if (viaggio.Depliant != null)
                    {
                        string fileName = String.Format("{0}.{1}", viaggio.Depliant.Id, viaggio.Depliant.NomeFile);
                        viaggio.Depliant.NomeFile = fileName;
                        SaveDepliant(viaggio);
                    }
                    om.CommitOperation();
                    logger.Info("Dati del viaggio {0} salvati con successo", viaggio);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Errore nel salvataggio del viaggio {0}", viaggio);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
        }

        private void SaveDepliant(Viaggio viaggio)
        {
            if (viaggio.Depliant.Id != 0 && viaggio.Agenzia.Id != 0)
            {
                string fileName = viaggio.Depliant.NomeFile;
                logger.Debug("Nome con cui verrà salvato il depliant: {0}", fileName);
                string fullPath = getDepliantFolder(viaggio.Agenzia);
                logger.Debug("Il file verrà salvato in {0}", fullPath);
                string fullPathFileName = System.IO.Path.Combine(fullPath, fileName);
                System.IO.File.WriteAllBytes(fullPathFileName, viaggio.Depliant.RawFile);
                logger.Info("Depliant salvato in {0}", fullPathFileName);
            }
            else
            {
                string msg = string.Format("Impossibile salvare il depliant del viaggio {0} in quanto il viaggio non è ancora stato salvato o non è associato ad un'agenzia.", viaggio);
                logger.Warn(msg);
                throw new Exception(msg);
            }
        }

        internal string getDepliantFolder(Agenzia agenzia)
        {
            var fullPath = System.IO.Path.Combine(Configurator.Istance.rootFolder,
                                           Configurator.Istance.companyIdPrefix + agenzia.Id.ToString(),
                                           Configurator.Istance.depliantFolder);
            if (!System.IO.Directory.Exists(fullPath))
            {
                logger.Debug("La directory {0} non esiste, quindi verrà creata", fullPath);
                createFolder(fullPath);
            }
            return fullPath;

        }

        private void createFolder(string fullPath)
        {
            System.IO.Directory.CreateDirectory(fullPath);
            /*
            if (!Configurator.Istance.isRootFolderRelative)
            {
                // assegnazione permessi di scrittura per l'utente corrente
                var user = System.Security.Principal.WindowsIdentity.GetCurrent().User;
                var userName = user.Translate(typeof(System.Security.Principal.NTAccount));
                var dirInfo = new System.IO.DirectoryInfo(fullPath);
                var sec = dirInfo.GetAccessControl();
                sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(userName,
                    System.Security.AccessControl.FileSystemRights.Modify,
                    System.Security.AccessControl.AccessControlType.Allow)
                    );
                dirInfo.SetAccessControl(sec);
            }
            */
            logger.Info("Directory {0} creata con successo", fullPath);
        }

        public void DeleteDepliant(int idDepliant)
        {
            logger.Debug("Richiesta di eliminazione del depliant {0}", idDepliant);
            using (var om = new OperationManager())
            {
                try
                {
                    var session = om.BeginOperation();
                    var viaggio = GetViaggioByDepliant(idDepliant);
                    logger.Debug("Il viaggio da cui il depliant {0} sarà rimosso è {1}", idDepliant, viaggio);
                    var depliant = viaggio.Depliant;                    
                    viaggio.Depliant = null;
                    var depliantPath = getDepliantFolder(viaggio.Agenzia);
                    var fullDepliantPath = System.IO.Path.Combine(depliantPath, depliant.NomeFile);
                    System.IO.File.Delete(fullDepliantPath);
                    vr.Save(viaggio);
                    vr.deleteDepliant(depliant);
                    om.CommitOperation();
                    logger.Info("Il depliant {0} relativo al viaggio {1} è stato eliminato", idDepliant, viaggio);
                }
                catch (Exception ex)
                {
                    om.RollbackOperation();
                    string msg = String.Format("Impossibile eliminare il depliant {0}", idDepliant);
                    logger.ErrorException(msg, ex);
                    throw new Exception(msg, ex);
                }
            }
            
        }
    }
}
