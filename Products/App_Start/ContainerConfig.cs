using Autofac;
using Autofac.Integration.Mvc;
using Products.DAL;
using Products.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Products.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder
            .RegisterType<ProductsDbContext>()
            .AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>))
               .As(typeof(IGenericRepository<>)).InstancePerRequest();
            builder.RegisterType<ProductRepo>()
            .As<IProductRepo>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}