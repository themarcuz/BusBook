using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Enums;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class MessaggioController : Controller
    {
        //
        // GET: /Messaggio/

        MessaggioRepository mr = new MessaggioRepository();

        public ActionResult List()
        {
            var loggedUtente = Session.getLoggedUtente();
            var mw = new MenuView(loggedUtente);
            foreach (var messaggio in mw.Messaggi)
            {
                messaggio.Stato = (int)MessaggioEnumerator.Letto;
                mr.Save(messaggio);
            }
            return View("List", mw);
        }

        [ChildActionOnly]
        public ActionResult ShowTile(Messaggio messaggio)
        {
            return PartialView(messaggio);
        }

        [HttpPost]
        public void Read(int id)
        {
            var mr = new MessaggioRepository();
            var messaggio = mr.GetById(id);
            messaggio.Stato = (int)MessaggioEnumerator.Letto;
            mr.Save(messaggio);
        }

        public void Delete(int id)
        {
            var messaggio = mr.GetById(id);
            messaggio.Stato = (int)MessaggioEnumerator.Cancellato;
            mr.Save(messaggio);
        }
    }
}
