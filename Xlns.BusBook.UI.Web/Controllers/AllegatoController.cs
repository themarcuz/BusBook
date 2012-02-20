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
<<<<<<< HEAD
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
=======
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idViaggio"></param>
        /// <param name="tipo">1: depliant, 2: immagine promozionale</param>
        /// <returns></returns>
        [ChildActionOnly]
<<<<<<< HEAD
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

=======
        public ActionResult New(int idViaggio, int tipo)
        {
            var avm = new AllegatoViewModel() { IdViaggio = idViaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            return PartialView(avm);
        }

>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c
        [ChildActionOnly]
        public ActionResult Show(int id)
        {

            return PartialView();
        }

        public ActionResult Upload(int idViaggio, int tipo)
        {
<<<<<<< HEAD
            var avm = new AllegatoViewModel() { Viaggio = new Viaggio() { Id = idViaggio }, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
=======
            var avm = new AllegatoViewModel() { IdViaggio = idViaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c
            return PartialView(avm);
        }

        [HttpPost]
<<<<<<< HEAD
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
                return RedirectToAction("Edit", new { allegato = allegato});
            }
            catch (Exception ex) 
            {
                String msg = "Impossibile caricare l'allegato";
                avm.Errore = msg;
                logger.ErrorException(msg, ex);
                return PartialView("Upload", avm);
            }
            
=======
        public ActionResult Save(int idViaggio, int tipo, HttpPostedFileBase file)
        {
            var avm = new AllegatoViewModel() { IdViaggio = idViaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            var vr = new ViaggioRepository();
            var viaggio = vr.GetById(idViaggio);
            Int32 length = file.ContentLength;
            byte[] rawFile = new byte[length];
            file.InputStream.Read(rawFile, 0, length);
            var allegato = new Allegato()
            {
                RawFile = rawFile,
                NomeFile = file.FileName
            };
            viaggio.Depliant = allegato;

            vr.Save(viaggio);

            return PartialView(avm);
>>>>>>> 1e9dd669bb4aed4d8ebcaced2eae34fc49b52e2c
        }

    }
}
