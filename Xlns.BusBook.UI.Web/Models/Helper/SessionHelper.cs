using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Xlns.BusBook.UI.Web.Models
{
    public static class SessionHelper
    {
        public static int getItemsPerPage(this HttpSessionStateBase session) 
        {
            return (int)session["maxPageNumer"];            
        }

        public static void setItemsPerPage(this HttpSessionStateBase session, int maxPageNumer)
        {
            session["maxPageNumer"] = maxPageNumer;
        }

        public static int getItemsPerPage(this HttpSessionState session)
        {
            return (int)session["maxPageNumer"];
        }

        public static void setItemsPerPage(this HttpSessionState session, int maxPageNumer)
        {
            session["maxPageNumer"] = maxPageNumer;
        }
    }
}