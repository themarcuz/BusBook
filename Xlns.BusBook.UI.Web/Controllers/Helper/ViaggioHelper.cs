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
    public static class ViaggioHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static ViaggioSearch getViaggioSearchParams(ViaggioSearchView searchViewModelParams, bool onlyViaggiPubblicati)
        {
            ViaggioSearch searchModelParams = null;

            if (searchViewModelParams != null)
            {
                searchModelParams = new ViaggioSearch()
                {
                    onlyPubblicati = onlyViaggiPubblicati,
                    SearchString = searchViewModelParams.SearchString,
                    DataPartenzaMin = searchViewModelParams.DataPartenzaMin,
                    DataPartenzaMax = searchViewModelParams.DataPartenzaMax,
                    PrezzoMin = searchViewModelParams.PrezzoMin,
                    PrezzoMax = searchViewModelParams.PrezzoMax,
                    PassaDa = getGeoLocationModelFromViewModel(searchViewModelParams.PassaDa),
                    ArrivaA = getGeoLocationModelFromViewModel(searchViewModelParams.ArrivaA)
                };
            }

            return searchModelParams;
        }

        public static GeoLocation getGeoLocationModelFromViewModel(GeoLocationView geoView)
        {
            GeoLocation geoModel = null;

            if (geoView != null && !geoView.isEmpty())
            {
                geoModel = new GeoLocation()
                {
                    CAP = geoView.CAP,
                    City = geoView.City,
                    Lat = geoView.Lat,
                    Lng = geoView.Lng,
                    Nation = geoView.Nation,
                    Number = geoView.Number,
                    Province = geoView.Province,
                    Region = geoView.Region,
                    Street = geoView.Street
                };
            }

            return geoModel;
        }

    }
}
