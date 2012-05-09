using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xlns.BusBook.UI.Web.Models;

namespace Xlns.BusBook.UI.Web.Controllers.Helper
{
    public class AuthenticationHelper
    {
        public static bool isLogged(HttpSessionStateBase session)
        {
            if (session.getLoggedUtente() != null)
                return true;
            else return false;
        }
    }
}