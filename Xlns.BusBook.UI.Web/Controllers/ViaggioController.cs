using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class ViaggioController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult List()
        {
            return View();
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

    }
}
