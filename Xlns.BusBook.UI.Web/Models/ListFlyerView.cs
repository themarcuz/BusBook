using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class ListFlyerView
    {
        public int idAgenzia;
        public IList<Flyer> flyers;
    }
}