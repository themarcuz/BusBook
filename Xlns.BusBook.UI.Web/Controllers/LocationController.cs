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
        public ActionResult Read(GeoLocation location, int mapWidth = 500, int mapHeight = 350)
        {
            var locationPartialView = new LocationPartialView() 
            {
                Location = location,
                MapWidth = mapWidth,
                MapHeight = mapHeight
            };
            return PartialView(locationPartialView);
        }

    }
}
