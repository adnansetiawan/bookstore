using BookStore.BLL;
using BookStore.Contracts;
using BookStore.Contracts.BLL;
using BookStore.Contracts.DAL;
using BookStore.DAL;
using BookStore.Entities.Indentity;
using BookStore.Services.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly : OwinStartup(typeof(BookStore.WebApi.Startup))]
namespace BookStore.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = SimpleInjectorResolver(app);
            ConfigureOAuth(app, container);
            HttpConfiguration config = new HttpConfiguration();
            //add this, if not you IOC not work
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile(new BLL.Mapping.DbEntityToDtoMapper());
                cfg.AddProfile(new BLL.Mapping.DtoToDbEntityMapper());
                cfg.AddProfile(new Mapper.DtoToModelMapper());
                cfg.AddProfile(new Mapper.ModelToDtoMapper());
            });
           
        }

        private Container SimpleInjectorResolver(IAppBuilder app)
        {
            
        

        // Create the container as usual.
        var container = new Container();
            app.Use(async (context, next) =>
            {
                using (var scope = container.BeginExecutionContextScope())
                {
                    await next.Invoke();
                }
            });
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            // Register your types, for instance using the scoped lifestyle:
            container.Register<IUnitOfWork>(()=>new EFUnitOfWork(), Lifestyle.Scoped);
            container.Register<ICategoryBLL, CategoryBLL>(Lifestyle.Scoped);
            container.Register<IBookBLL, BookBLL>(Lifestyle.Scoped);
            container.Register<IApplicationUserManager, ApplicationUserManager>(Lifestyle.Singleton);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationEntities()), Lifestyle.Singleton);
            container.Register<IdentityFactoryOptions<ApplicationUserManager>>(() => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("BookStore API")

            }, Lifestyle.Singleton);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();
             GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            return container;
            
        }
        private void ConfigureOAuth(IAppBuilder app, Container container)
        {
            var userManager = container.GetInstance<IApplicationUserManager>();
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(userManager)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }


    }
}