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
        
        AllegatoRepository ar = new AllegatoRepository();

        public ActionResult Download(int id) {
            var allegato = ar.GetAllegatoById(id);
            return File(allegato.RawFile, "application/octet-stream");
        }

       

    }
}
