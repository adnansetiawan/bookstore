using AutoMapper;
using BookStore.BLL;
using BookStore.Contracts.BLL;
using BookStore.Contracts.DAL;
using BookStore.DAL;
using BookStore.WebApi.Models.Request.Category.Create;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace BookStore.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SimpleInjectorResolver();
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.AddProfile(new BLL.Mapping.DbEntityToDtoMapper());
                cfg.AddProfile(new BLL.Mapping.DtoToDbEntityMapper());
                cfg.AddProfile(new Mapper.DtoToModelMapper());
                cfg.AddProfile(new Mapper.ModelToDtoMapper());
            });
        }

        private void SimpleInjectorResolver()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IUnitOfWork>(()=>new EFUnitOfWork());

            container.Register<ICategoryBLL, CategoryBLL>();
            container.Register<IBookBLL, BookBLL>();

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
