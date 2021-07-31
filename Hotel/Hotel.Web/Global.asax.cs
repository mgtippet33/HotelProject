using Hotel.BLL.Infrastructure;
using Hotel.Web.Utils;
using HotelWEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hotel.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule clientModule = new ClientModule();
            NinjectModule usertModule = new UserModule();
            NinjectModule categoryModule = new CategoryModule();
            NinjectModule priceCategoryModule = new PriceCategoryModule();
            NinjectModule roomModule = new RoomModule();
            NinjectModule reservationModule = new ReservationModule();
            NinjectModule dependencyModule = new DependencyModule("HotelModel");

            var kernel = new StandardKernel(dependencyModule, categoryModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));

            //var kernel = new StandardKernel(clientModule, usertModule, categoryModule,
            //    priceCategoryModule, roomModule, reservationModule, dependencyModule);
            //kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
            //kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
            //GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);

        }
    }
}
