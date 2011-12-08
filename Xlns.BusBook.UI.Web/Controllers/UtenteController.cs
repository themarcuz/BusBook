using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Crypto;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.UI.Web.Models.Helper;
using Xlns.BusBook.Core.Model;

namespace Xlns.BusBook.UI.Web.Controllers
{
    public class UtenteController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //
        // GET: /Utente/

        public ActionResult Index(string q, string ini)
        {
            return RedirectToAction("List");
        }


        public ActionResult List(string q, string ini)
        {
            logger.Debug("Caricamento utenti con iniziale='{0}' e filtro='{1}'", ini, q);
            if (string.IsNullOrEmpty(q)) q = "";
            if (string.IsNullOrEmpty(ini)) ini = "";
            var utenteService = new UtenteRepository();
            var myModel = utenteService.GetAllUtenti(q, ini);
            var utenti = new UtenteView(myModel.ToList(), q, ini);
            return View(utenti);
        }

        public ActionResult Search(string q, string ini)
        {
            logger.Debug("Caricamento utenti con iniziale='{0}' e filtro='{1}'", ini, q);
            System.Threading.Thread.Sleep(1000);
            if (string.IsNullOrEmpty(q)) q = "";
            if (string.IsNullOrEmpty(ini)) ini = "";
            var utenteService = new UtenteRepository();
            var myModel = utenteService.GetAllUtenti(q, ini);
            var utenti = new UtenteView(myModel.ToList(), q, ini);
            return PartialView(utenti);
        }

        public ActionResult Detail(int id)
        {
            var ur = new UtenteRepository();
            Utente utente = ur.GetById(id);
            return View(utente);
        }

        public ActionResult Delete(int id)
        {
            var ur = new UtenteRepository();
            Utente utente = ur.GetById(id);
            ur.Delete(utente);
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            var myModel = new Utente();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult Create(Utente utente)
        {
            if (ModelState.IsValid)
            {
                var uRepository = new UtenteRepository();
                var cryptyo = new CryptoHelper();
                utente.Password = cryptyo.cryptPassword(utente.Password);
                uRepository.Save(utente);
                return RedirectToAction("List");
            }
            return View(utente);
        }

        public ActionResult Edit(int id)
        {
            var ur = new UtenteRepository();
            Utente utente = ur.GetById(id);
            var myModel = utente;
            return View(myModel);
        }

        [HttpPost]
        public ActionResult Edit(Utente utente)
        {
            if (ModelState.IsValid)
            {
                var uRepository = new UtenteRepository();
                uRepository.Save(utente);
                return RedirectToAction("List");
            }
            return View(utente);
        }

        public ActionResult ChangePassword(int id)
        {
            var ur = new UtenteRepository();
            Utente utente = ur.GetById(id);
            var myModel = utente;
            return View(myModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(Utente utente)
        {
            var newPassword = utente.Password;
            var ur = new UtenteRepository();
            var cryptyo = new CryptoHelper();
            utente = ur.GetById(utente.Id);
            utente.Password = cryptyo.cryptPassword(newPassword);
            ur.Save(utente);
            return RedirectToAction("List");
        }
    }
}
