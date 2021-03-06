﻿using System;
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
            {
                MenuView menuView = new MenuView(loggedUtente);
                return PartialView("Logout", menuView);
            }
        }

        [HttpPost]
        public ActionResult Login(UtenteLoginView utente, bool redirectOnSuccess)
        {
            if (ModelState.IsValid)
            {

                var authResult = LoginHelper.AuthenticateUtente(utente.Username, utente.Password);

                if (authResult.IsAuthenticated)
                {
                    Session.Login(authResult.AuthenticatedUtente);
                    if (redirectOnSuccess)
                    {
                        ViewBag.RedirectUrl = Url.Action("DashBoard", "Home");
                        return PartialView("Redirect");
                    }
                }
                else
                {
                    utente.LoginErrorMessage = authResult.AuthErrorMessage;
                    utente.Password = "";
                }
            }
            
            return ShowLogin(utente);
        }

        [HttpPost]
        public ActionResult Logout(bool redirectOnSuccess)
        {
            Session.Logout();
            if (redirectOnSuccess)
            {
                ViewBag.RedirectUrl = Url.Action("Index", "Home");
                return PartialView("Redirect");
            }
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
                    var message = ConfigurationManager.Configurator.Istance.mailBodyChangePassword.Replace("{password}", newPassword);
                    mh.SendMail(u.Email, message);
                }
            }
            return View(u);
        }

        public ActionResult Registration()
        {
            RegistrationView registration = new RegistrationView();
            return View(registration);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView registration)
        {
            if (ModelState.IsValid)
            {
                if (!ReCaptcha.Validate(privateKey: ConfigurationManager.Configurator.Istance.recaptchaPublicKey))
                {
                }
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
                    utente.Email = registration.Utente.Email;
                    utente.Agenzia = agenzia;
                    var cryptedPassword = crypto.cryptPassword(registration.Utente.Password);
                    utente.Password = cryptedPassword;
                    ur.Save(utente);
                    var message = ConfigurationManager.Configurator.Istance.mailBodyRegister;
                    mh.SendMail(utente.Email, message);
                    ViewBag.RedirectUrl = Url.Action("Index", "Home");
                    return View("Redirect");
                }
            }
            return View(registration);
        }

        public ActionResult Facebook()
        {
            var facebookApplicationId = ConfigurationManager.Configurator.Istance.facebookApplicationId;
            ViewBag.Facebook = facebookApplicationId;
            return PartialView();
        }
    }
}
