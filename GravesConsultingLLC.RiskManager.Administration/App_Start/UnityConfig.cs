using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace GravesConsultingLLC.RiskManager.Administration
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IRepository, Repository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}