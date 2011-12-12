using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ViaggioRepository vr = new ViaggioRepository();

        public ActionResult List()
        {
            var viaggi = vr.GetViaggi();
            return View(viaggi);
        }

        public ActionResult Create()
        {
            var viaggio = new Viaggio();
            viaggio.Tappe = new List<Tappa>()
            {
                new Tappa() { Id = 1, Tipo = TipoTappa.PARTENZA, Ordinamento = 1},
                new Tappa() { Id = 2, Tipo = TipoTappa.DESTINAZIONE, Ordinamento = 2},
            };
            return View(viaggio);
        }

        [ChildActionOnly]
        public ActionResult TappaEdit(Tappa tappa)
        {
            return PartialView(tappa);
        }

        public ActionResult TappaDetailTest()
        {
            var viaggi = vr.GetViaggi();
            var tappa = viaggi[0].Tappe[1];
            return View("TappaDetail", tappa);
        }
        
        [ChildActionOnly]
        public ActionResult TappaDetail(Tappa tappa)
        {
            return PartialView(tappa);
        }

        [ChildActionOnly]
        public ActionResult ViaggioTiledDetail(Viaggio viaggio) {
            return PartialView(viaggio);
        }

    }
}
