using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Features.Startup))]

namespace Features
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;
            //Configure AutoFac (http://autofac.org/) for DependencyResolver
            //For more information visit http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver

            app.MapSignalR();

            ConfigureComposition(config);

            ConfigureAuth(app);


        }
    }
}
