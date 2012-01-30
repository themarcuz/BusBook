using System;
using System.Collections.Generic;
using System.Linq;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Crypto;

namespace Xlns.BusBook.Core.Login
{
    public class LoginHelper
    {

        public static AuthenticationResult AuthenticateUtente(String username, String passwordInChiaro)
        {
            AuthenticationResult result = null;

            UtenteRepository ur = new UtenteRepository();
            var registeredUtente = ur.GetByUsername(username);

            if (registeredUtente == null)
            {
                result = new AuthenticationResult { IsAuthenticated = false, AuthErrorMessage = "Username/Password errata!" };
            }
            else
            {
                CryptoHelper crypter = new CryptoHelper();
                if (crypter.cryptPassword(passwordInChiaro).Equals(registeredUtente.Password))
                    result = new AuthenticationResult { IsAuthenticated = true, AuthenticatedUtente = registeredUtente };
                else
                    result = new AuthenticationResult { IsAuthenticated = false, AuthErrorMessage = "Username/Password errata!"};

            }

            return result;
        }
    }
}