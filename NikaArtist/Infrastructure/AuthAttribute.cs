﻿using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace NikaArtist.Infrastructure
{
    public class AuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = HttpContext.Current.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Auth", new { returnUrl = filterContext.HttpContext.Request.RawUrl }));
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}