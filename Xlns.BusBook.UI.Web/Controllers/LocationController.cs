using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.UI.Web.Models;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class LocationController : Controller
    {        
        [ChildActionOnly]
        public ActionResult Show(LocationPartialView locationPartialView)
        {           
            return PartialView(locationPartialView);
        }

        [ChildActionOnly]
        public ActionResult Edit(LocationPartialView locationPartialView)
        {           
            return PartialView(locationPartialView);
        }

    }
}
