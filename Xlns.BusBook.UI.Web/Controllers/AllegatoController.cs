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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idViaggio"></param>
        /// <param name="tipo">1: depliant, 2: immagine promozionale</param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult New(int idViaggio, int tipo)
        {
            var avm = new AllegatoViewModel() { IdViaggio = idViaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            return PartialView(avm);
        }

        [ChildActionOnly]
        public ActionResult Show(int id)
        {

            return PartialView();
        }

        public ActionResult Upload(int idViaggio, int tipo)
        {
            var avm = new AllegatoViewModel() { IdViaggio = idViaggio, Tipo = (AllegatoViewModel.TipoAllegato)tipo };
            return PartialView(avm);
        }

        [HttpPost]
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
        }

    }
}
