﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Spring.Social.OAuth2;
using Spring.Social.Facebook.Api;
using Spring.Social.Facebook.Connect;

namespace Spring.MvcQuickStart.Controllers
{
    public class FacebookController : Controller
    {
        // Register your own Facebook app at https://developers.facebook.com/apps
        // Configure the Callback URL with 'http://localhost/Facebook/Callback'
        // Set your application id & secret here
        private const string FacebookApiId = TODO;
        private const string FacebookApiSecret = TODO;

        IOAuth2ServiceProvider<IFacebook> facebookProvider =
            new FacebookServiceProvider(FacebookApiId, FacebookApiSecret);

        // GET: /Facebook/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Facebook/SignIn
        public ActionResult SignIn()
        {
            OAuth2Parameters parameters = new OAuth2Parameters()
            {
                RedirectUrl = "http://localhost/Facebook/Callback",
                Scope = "publish_stream"
            };
            return Redirect(facebookProvider.OAuthOperations.BuildAuthorizeUrl(GrantType.AuthorizationCode, parameters));
        }

        // GET: /Facebook/Callback
        public ActionResult Callback(string code)
        {
            AccessGrant accessGrant = facebookProvider.OAuthOperations.ExchangeForAccessAsync(
                code, "http://localhost/Facebook/Callback", null).Result;

            Session["AccessGrant"] = accessGrant;

            return View();
        }
    }
}
