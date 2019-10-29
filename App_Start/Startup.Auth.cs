using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Features.Providers;
using Features.Models;

namespace Features
{
    public partial class Startup
    {
        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions();
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
