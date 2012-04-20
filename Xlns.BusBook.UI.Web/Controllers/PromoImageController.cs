using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core;
using Xlns.BusBook.Core.Repository;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class PromoImageController : Controller
    {
        ViaggioManager vm = new ViaggioManager();

        public ActionResult View(int id)
        {
            var ar = new AllegatoRepository();
            var allegato = ar.GetAllegatoWithFileById(id);
            return new FileContentResult(allegato.RawFile, "application/octet-stream");
        }

        public ActionResult Edit(int idPromoImage, String htmlId)
        {            
            var viaggio = vm.GetViaggioByPromoImage(idPromoImage);
            var pivm = new Models.PromoImageViewModel() 
            {
                HtmlContainerId = htmlId,
                Viaggio = viaggio
            };
            return PartialView(pivm);
        }

        public ActionResult Delete(int id) 
        {
            vm.DeletePromoImage(id);                        
            return PartialView("New");
        }

        public ActionResult New() 
        {
            return PartialView();
        }

    }
}
