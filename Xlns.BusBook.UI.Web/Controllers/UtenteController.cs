using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Crypto;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.UI.Web.Models.Helper;
using Xlns.BusBook.Core.Mailer;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class UtenteController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        UtenteRepository ur = new UtenteRepository();

        public ActionResult Index(string q, string ini)
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string q, string ini)
        {
            //logger.Debug("Caricamento utenti con iniziale='{0}' e filtro='{1}'", ini, q);
            //if (string.IsNullOrEmpty(q)) q = "";
            //if (string.IsNullOrEmpty(ini)) ini = "";
            //var myModel = ur.GetAllUtenti(q, ini);
            var utenti = new UtenteView(new List<Utente>(), q, ini);
            return View(utenti);
        }

        public ActionResult Search(string q, string ini)
        {
            logger.Debug("Caricamento utenti con iniziale='{0}' e filtro='{1}'", ini, q);
            System.Threading.Thread.Sleep(1000);
            if (string.IsNullOrEmpty(q)) q = "";
            if (string.IsNullOrEmpty(ini)) ini = "";
            var myModel = ur.GetAllUtenti(q, ini);
            var utenti = new UtenteView(myModel.ToList(), q, ini);
            return PartialView(utenti);
        }

        public ActionResult Detail(int id)
        {
            Utente utente = ur.GetById(id);
            return View(utente);
        }

        public void Delete(int id)
        {
            try
            {
                Utente utente = ur.GetById(id);
                ur.Delete(utente);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione dell'utente con id={0}", id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg);
            }
        }

        public ActionResult Create()
        {
            Utente utente = new Utente();
            return View(utente);
        }

        [HttpPost]
        public ActionResult Create(Utente utente)
        {
            if (ModelState.IsValid)
            {
                var cryptyo = new CryptoHelper();
                utente.Password = cryptyo.cryptPassword(utente.Password);
                ur.Save(utente);
                return RedirectToAction("List");
            }
            return View(utente);
        }

        public ActionResult Edit(int id)
        {
            Utente utente = ur.GetById(id);
            return View(utente);
        }

        [HttpPost]
        public ActionResult Edit(Utente utente)
        {
            if (ModelState.IsValid)
            {
                UtenteManager um = new UtenteManager();
                um.Save(utente);
                return RedirectToAction("List");
            }
            return View(utente);
        }

        public ActionResult ChangePassword(int id)
        {
            Utente utente = ur.GetById(id);
            var password = new DettaglioPasswordView() { userId = id};
            return View(password);
        }

        [HttpPost]
        public ActionResult ChangePassword(DettaglioPasswordView dettaglioPassword, int id)
        {

            if (ModelState.IsValid)
            {
                Utente utente = ur.GetById(id);
                var chrypto = new CryptoHelper();

                if (utente.Password.Equals(chrypto.cryptPassword(dettaglioPassword.Password)))
                {
                    var newPassword = dettaglioPassword.newPassword;
                    var repeatNewPassword = dettaglioPassword.repeatNewPassword;
                    if (newPassword.Equals(repeatNewPassword))
                    {

                        utente.Password = chrypto.cryptPassword(newPassword);
                        ur.Save(utente);
                        MailHelper mh = new MailHelper();
                        //mh.SendChangedPasswordEmail(utente.Agenzia.Email);
                        return RedirectToAction("List");
                    }
                    else //questo non può mai succedere perchè c'è la validazione sul modello...
                        ModelState.AddModelError(String.Empty, "Le password inserite non corrispondono!");

                }
                else//questo non dovrebbe mai succedere perchè c'è la validazione Remote sul modello...
                    ModelState.AddModelError(String.Empty, "La password attuale non corrisponde con quella inserita!");
            }
            return View(dettaglioPassword);
        }

        [ChildActionOnly]
        public ActionResult ShowTile(DettaglioUtenteView utenteVM)
        {
            return PartialView(utenteVM);
        }

        public void DeleteAjax(int id)
        {
            try
            {
                ur.Delete(id);
            }
            catch (Exception ex)
            {
                string msg = String.Format("Errore durante l'eliminazione dell'utente con id={0}", id);
                logger.ErrorException(msg, ex);
                throw new Exception(msg);
            }
        }

        public JsonResult CheckPassword(String password, int userId)
        {
            bool valid = false;

            Utente utente = ur.GetById(userId);
            if (utente != null)
            {
                var chrypto = new CryptoHelper();
                if (utente.Password.Equals(chrypto.cryptPassword(password)))
                    valid = true;
            }

            return Json(valid, JsonRequestBehavior.AllowGet);
        }
    }
}
