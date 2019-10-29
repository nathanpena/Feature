using Autofac;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using Features.Common;
using Features.Providers;

namespace Features
{
    public partial class Startup
    {
        private static IContainer RegisterServices()
        {
            string _adServ = WebConfigurationManager.AppSettings["UTRGV_AD_ADDRESS"];
            string _emailServ = WebConfigurationManager.AppSettings["UTRGV_EMAILSERV_ADDRESS"];
            string _thisServ = WebConfigurationManager.AppSettings["UTRGV_THIS_ADDRESS"];



            var builder = new ContainerBuilder();


            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .Where(t => t.Name.EndsWith("Controller"))
                .AsSelf();


            builder.RegisterInstance(new LDAPLoginProvider())
                .As<ILoginProvider>()
               .SingleInstance();





            return builder.Build();
        }
        public static void ConfigureComposition(HttpConfiguration config)
        {
            IContainer container = RegisterServices();
            config.DependencyResolver = new AutoFacDependencyResolver(container);
        }
    }
}
