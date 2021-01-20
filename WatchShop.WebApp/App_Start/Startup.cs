using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using WatchShop.Application.Services;
using WatchShop.Data.Infrastructure;
using WatchShop.Data.Models;
using WatchShop.Data.Repositories;

[assembly: OwinStartup(typeof(WatchShop.WebApp.App_Start.Startup))]

namespace WatchShop.WebApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAuth(app);
            ConfigAutofac(app);
        }

        private void ConfigAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Home/Index"),
                SlidingExpiration = true
            });
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "377254028130-84lqckkfk6ljrrtcslqnf5a2c64u381q.apps.googleusercontent.com",
                ClientSecret = "jpuk4FNJ0S72Ms4lEr-X39ct",
                //CallbackPath = new PathString("/signin-google")
            });
        }    

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();


            // Implement DI Repository
            builder.RegisterAssemblyTypes(typeof(DongHoRepository).Assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Implement DI Service
            builder.RegisterAssemblyTypes(typeof(DongHoService).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }    
    }
}
