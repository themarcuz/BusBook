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

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        UtenteRepository ur = new UtenteRepository();
        CryptoHelper crypto = new CryptoHelper();
        MailHelper mh = new MailHelper();

        public ActionResult Login()
        {
            var myModel = new Utente();
            return PartialView(myModel);
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

        public ActionResult Register()
        {
            RegistrationView registration = new RegistrationView();
            return View(registration);
        }

        public ActionResult Informativa()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationView registration)
        {
            if (ModelState.IsValid)
            {
                if (!ReCaptcha.Validate(privateKey: ConfigurationManager.Configurator.Istance.recaptchaPublicKey))
                {
                    if (registration.Utente.Password.Equals(registration.UtenteRepeatPassword))
                    {
                        Utente utente = new Utente();
                        utente.Nome = registration.Utente.Nome;
                        utente.Cognome = registration.Utente.Cognome;
                        utente.Username = registration.Utente.Username;
                        var cryptedPassword = crypto.cryptPassword(registration.Utente.Password);
                        utente.Password = cryptedPassword;
                    }
                }
            }
            return View(registration);
        }
    }
}
