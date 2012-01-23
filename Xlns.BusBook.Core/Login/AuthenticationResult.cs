using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.Core.Login
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public String AuthErrorMessage { get; set; }
        public Utente AuthenticatedUtente { get; set; }
    }
}
