using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Models
{
    public class DepliantViewModel
    {
        public Viaggio Viaggio { get; set; }
        public String HtmlContainerId { get; set; }
        public bool ShowDeleteCommand { get; set; }
        
    }
}