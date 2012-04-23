using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xlns.BusBook.UI.Web.Models;
using Xlns.BusBook.UI.Web.Controllers.Helper;

namespace Xlns.BusBook.UI.Web.Controllers.Filters
{
    public class AuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {

        private string[] _acceptedRoles;

        public AuthorizationFilterAttribute(params string[] acceptedRoles)
        {
            _acceptedRoles = acceptedRoles;
        }

        public AuthorizationFilterAttribute()
        {

        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthenticationHelper.isLogged(filterContext.HttpContext.Session))
                throw new UnauthorizedAccessException();
            

        }        
    }
}