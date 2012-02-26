using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class AllegatoController : Controller
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idViaggio"></param>
        /// <param name="tipo">1: depliant, 2: immagine promozionale</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult New(Viaggio viaggio, int tipo)
        {
            var avm = new AllegatoViewModel() { Viaggio = viaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            return PartialView(avm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idViaggio"></param>
        /// <param name="tipo">1: depliant, 2: immagine promozionale</param>
        /// <returns></returns>        
        public ActionResult Edit(Viaggio viaggio)
        {
            return PartialView(viaggio);
        }

        [ChildActionOnly]
        public ActionResult Show(int id)
        {

            return PartialView();
        }

        public ActionResult Upload(int idViaggio, int tipo)
        {
            var avm = new AllegatoViewModel() { Viaggio = new Viaggio() { Id = idViaggio }, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            return PartialView(avm);
        }

        [HttpPost]
        public ActionResult Save(AllegatoViewModel avm, HttpPostedFileBase file)
        {
            try
            {
                var vr = new ViaggioRepository();
                var idViaggio = avm.Viaggio.Id;
                var viaggio = vr.GetById(idViaggio);
                avm.Viaggio = viaggio;
                logger.Info("Caricamento allegato per il viaggio {0}", viaggio);
                Int32 length = file.ContentLength;
                byte[] rawFile = new byte[length];
                file.InputStream.Read(rawFile, 0, length);
                var allegato = new AllegatoViaggio()
                {
                    RawFile = rawFile,
                    NomeFile = file.FileName,
                    Viaggio = viaggio
                };
                viaggio.Depliant = allegato;

                vr.Save(viaggio);
                //return RedirectToAction("Edit", new { allegato = allegato.Viaggio });
                return PartialView("Edit", allegato.Viaggio);
            }
            catch (Exception ex)
            {
                String msg = "Impossibile caricare l'allegato";
                avm.Errore = msg;
                logger.ErrorException(msg, ex);
                return PartialView("Upload", avm);
            }
        }

    }
}
