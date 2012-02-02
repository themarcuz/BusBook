using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Xlns.BusBook.Core.Model;

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

        private static T getDataFromSession<T>(this HttpSessionStateBase session, string key)
        {
            return (T)session[key];
        }

        private static void setDataInSession<T>(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }

        public static Utente getLoggedUtente(this HttpSessionStateBase session)
        {
            return getDataFromSession<Utente>(session, "loggedUtente");
        }

        public static Utente getLoggedUtente(this HttpSessionState session)
        {
            return (Utente)session["loggedUtente"];
        }

        public static Agenzia getLoggedAgenzia(this HttpSessionState session)
        {
            var utente = getLoggedUtente(session);
            return utente == null ? null : utente.Agenzia;
        }

        public static Agenzia getLoggedAgenzia(this HttpSessionStateBase session)
        {
            var utente = getLoggedUtente(session);
            return utente == null ? null : utente.Agenzia;
        }

        public static void Login(this HttpSessionStateBase session, Utente utente)
        {
            setDataInSession<Utente>(session, "loggedUtente", utente);
        }

        public static void Logout(this HttpSessionStateBase session)
        {
            session.Remove("loggedUtente");
        }
    }
}