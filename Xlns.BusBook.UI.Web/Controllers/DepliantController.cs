using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class DepliantController : Controller
    {
        ViaggioManager vm = new ViaggioManager();

        public ActionResult View(int idDepliant)
        {
            var viaggio = vm.GetViaggioByDepliant(idDepliant);
            var dvm = new Models.DepliantViewModel()
            {                
                Viaggio = viaggio
            };
            return PartialView("Edit", dvm);
        }

        public ActionResult Edit(int idDepliant, String htmlId)
        {            
            var viaggio = vm.GetViaggioByDepliant(idDepliant);
            var dvm = new Models.DepliantViewModel() 
            {
                HtmlContainerId = htmlId,
                Viaggio = viaggio,
                ShowDeleteCommand = true
            };
            return PartialView(dvm);
        }

        public ActionResult Delete(int id) 
        {
            vm.DeleteDepliant(id);                        
            return PartialView("New");
        }

        public ActionResult New() 
        {
            return PartialView();
        }

    }
}
