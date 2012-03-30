using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xlns.BusBook.Core.Repository;
using Xlns.BusBook.Core.Model;
using Xlns.BusBook.Core.DAL;
using Xlns.BusBook.ConfigurationManager;
using Xlns.BusBook.UI.Web.Controllers;

namespace Xlns.BusBook.UI.Web.Models
{
    public static class FlyerHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<ViaggioSelectView> getViaggiSelezionabili(Flyer flyer, List<Viaggio> viaggi)
        {
            List<ViaggioSelectView> viaggiSelezionabili = new List<ViaggioSelectView>();

            foreach (var viaggio in viaggi)
            {
                bool selected = false;

                if (flyer.Viaggi != null && flyer.Viaggi.Any(v => v.Id == viaggio.Id))
                    selected = true;

                ViaggioSelectView viaggioSelezionabile = new ViaggioSelectView() { viaggio = viaggio, isSelected = selected, isSelectable = true, idFlyer = flyer.Id, isDetailExternal = false };
                viaggiSelezionabili.Add(viaggioSelezionabile);
            }

            return viaggiSelezionabili;
        }

        public static void ToggleViaggio(Flyer flyer, Viaggio viaggio){

            if (flyer.Viaggi.Any(v => v.Id == viaggio.Id))
                flyer.Viaggi.Remove(viaggio);
            else
                flyer.Viaggi.Add(viaggio);
        }

        public static List<ViaggioSelectView> getViaggiSelezionati(int idFlyer, bool isDetailExternal)
        {
            var flyerRepo = new FlyerRepository();
            var flyer = flyerRepo.GetById(idFlyer);

            List<ViaggioSelectView> viaggiSelezionati = new List<ViaggioSelectView>();

            foreach (var viaggioSel in flyer.Viaggi)
            {
                ViaggioSelectView viaggioSelezionato = new ViaggioSelectView() { viaggio = viaggioSel, isSelected = true, isSelectable = false, idFlyer = flyer.Id, isDetailExternal = isDetailExternal };
                viaggiSelezionati.Add(viaggioSelezionato);
            }

            return viaggiSelezionati;
        }

    }
}
