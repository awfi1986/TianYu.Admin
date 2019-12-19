using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System; 
using System.IO;
using System.Linq;
using System.Reflection; 
using System.Web.Http;
using System.Web.Mvc;

namespace TianYu.Core.Common
{
    public class AutofacRegister
    {

        protected static IContainer Container
        {
            get;
            set;
        }
        /// <summary>
        /// autoFac 依赖注入
        /// </summary>
        public static void RegisterAutoFac(string application = "WEBAPI", HttpConfiguration config = null)
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(assemblies)
            //       .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
            //       .AsSelf().AsImplementedInterfaces()
            //       .PropertiesAutowired().InstancePerLifetimeScope();
            IContainer container = null;

            if (application == "WEBAPI")
            {
                builder.RegisterApiControllers(assemblies).PropertiesAutowired();
                builder.RegisterWebApiFilterProvider(config);
                container = builder.Build();
                config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            }
            else if (application == "WEBMVC")
            {
                builder.RegisterControllers(assemblies).PropertiesAutowired();
                container = builder.Build();
            }
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            Container = container;
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
