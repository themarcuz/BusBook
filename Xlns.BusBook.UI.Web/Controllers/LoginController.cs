using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Crypto;
using Xlns.BusBook.Core.Mailer;
using System.Web.Security;
using Xlns.BusBook.UI.Web.Models;
using Microsoft.Web.Helpers;
using Xlns.BusBook.Core.Login;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        AgenziaRepository ar = new AgenziaRepository();
        UtenteRepository ur = new UtenteRepository();
        CryptoHelper crypto = new CryptoHelper();
        MailHelper mh = new MailHelper();

        [ChildActionOnly]
        public ActionResult ShowLogin()
        {
            return ShowLogin(null);
        }

        private ActionResult ShowLogin(UtenteLoginView utente)
        {
            var loggedUtente = Session.getLoggedUtente();

            if (loggedUtente == null)
                return PartialView("Login", (utente == null ? new UtenteLoginView() : utente));
            else
                return PartialView("Logout", loggedUtente);
        }

        [HttpPost]
        public ActionResult Login(UtenteLoginView utente)
        {
            if (ModelState.IsValid)
            {

                var authResult = LoginHelper.AuthenticateUtente(utente.Username, utente.Password);

                if (authResult.IsAuthenticated)
                    Session.Login(authResult.AuthenticatedUtente);
                else
                {
                    utente.LoginErrorMessage = authResult.AuthErrorMessage;
                    utente.Password = "";
                }
            }

            //TODO: deve funzionare anche senza la validazione lato client!
            return ShowLogin(utente);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Logout();
            return ShowLogin();
        }

        public ActionResult ResetPassword()
        {
            Utente utente = new Utente();
            return View(utente);
        }

        [HttpPost]
        public ActionResult ResetPassword(Utente u)
        {
            if (ModelState.IsValid)
            {
                var user = ur.GetByUsername(u.Username);
                if (user != null)
                {
                    var newPassword = Membership.GeneratePassword(12, 0);
                    user.Password = crypto.cryptPassword(newPassword);
                    ur.Save(user);
                    //mh.SendResetPasswordEmail(u.Email, newPassword);
                }
            }
            return View(u);
        }

        public ActionResult Registration()
        {
            RegistrationView registration = new RegistrationView();
            return PartialView(registration);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registration)
        {
            if (ModelState.IsValid)
            {
                if (!ReCaptcha.Validate(privateKey: ConfigurationManager.Configurator.Istance.recaptchaPublicKey))
                {
                    if (registration.Utente.Password.Equals(registration.UtenteRepeatPassword))
                    {
                        Agenzia agenzia = new Agenzia();
                        agenzia.Nome = registration.Agenzia.Nome;
                        agenzia.RagioneSociale = registration.Agenzia.RagioneSociale;
                        agenzia.PIva = registration.Agenzia.PIva;
                        agenzia.Email = registration.Agenzia.Email;
                        ar.Save(agenzia);
                        Utente utente = new Utente();
                        utente.Nome = registration.Utente.Nome;
                        utente.Cognome = registration.Utente.Cognome;
                        utente.Username = registration.Utente.Username;
                        utente.Agenzia = agenzia;
                        var cryptedPassword = crypto.cryptPassword(registration.Utente.Password);
                        utente.Password = cryptedPassword;
                        ur.Save(utente);
                    }
                }
            }
            return View(registration);
        }
    }
}
