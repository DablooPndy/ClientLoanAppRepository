using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Http;
//using Autofac;
using System.Reflection;
using AutoMapper;
//using Autofac.Integration.Mvc;
using System.Web.Http.Controllers;
using Autofac;
using Autofac.Integration.Mvc;
using Client.LoanApplication.OpenAPIConfiguration;
using Autofac.Features.ResolveAnything;

namespace Client.LoanApplication
{
    /// <summary>
    /// Autofac Configuration class
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Method for Register Dependencies
        /// </summary>
        public static void RegisterDependencies()
        {
            SetupAutofacContainer();
        }
        /// <summary>
        /// Register all Dependencies and configure the Dependency Resolver
        /// </summary>
        public static void SetupAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            RegisterServices(builder);
            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        private static void RegisterServices(ContainerBuilder builder)
        {
            // Register Classess
            builder.RegisterType<Underwriterclient>().As<IUnderwriterClient>().InstancePerRequest();
            builder.RegisterType<Brokerclient>().As<IBrokerClient>().InstancePerRequest();

            // Register Automapper Configuration
            builder.RegisterInstance(AutomapperConfiguration.Configure());
        }
    }
}