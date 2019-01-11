using Common.Interfaces;
using DAL.Repositories.SqlRepositories;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //SimpleInjector registrations.
            container.Register<IAbstractBooksRepository, SqlAbstractBooksRepository>(Lifestyle.Singleton);
            container.Register<ICustomersRepository, SqlCustomersRepository>(Lifestyle.Singleton);
            container.Register<IEmployeesRepository, SqlEmployeesRepository>(Lifestyle.Singleton);
            container.Register<IPurchasesRepository, SqlPurchasesRepository>(Lifestyle.Singleton);
            container.Register<ILogsRepository, SqlLogsRepository>(Lifestyle.Singleton);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
