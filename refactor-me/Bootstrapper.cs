using System.Web.Http;
using Microsoft.Practices.Unity;
using Resolver;
using Unity.Mvc3;

namespace refactor_me
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            
            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            //Component initialization via MEF
            ComponentLoader.LoadContainer(container, ".\\bin", "refactor-me.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "BusinessAccessLayer.dll");
            ComponentLoader.LoadContainer(container, ".\\bin", "DataAccessLayer.dll");
        }
    }
}