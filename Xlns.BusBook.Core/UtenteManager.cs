using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.Repository;

namespace Xlns.BusBook.Core
{
    public class UtenteManager
    {
        public void Save(Utente utente)
        {
            if (utente.Id != 0 && utente.Agenzia == null)
            { 
                AgenziaRepository ar = new AgenziaRepository();
                utente.Agenzia = ar.GetById(utente.Agenzia.Id);
                UtenteRepository ur = new UtenteRepository();
                ur.Save(utente);
            }
        }
    }
}
