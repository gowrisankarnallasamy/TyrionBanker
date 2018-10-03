using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Database;
using TyrionBanker.Infrastructure.Database;
using TyrionBanker.Infrastructure.Repository;
using Unity;
using Unity.Injection;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.PolicyInjection;
using Unity.Lifetime;
using Unity.Registration;
using Unity.RegistrationByConvention;

namespace TyrionBanker.Infrastructure
{
    public static class InfrastructureUnityContainer
    {
        internal static IUnityContainer UnityContainer;
        public static void BuildUp(IUnityContainer container)
        {
            if (UnityContainer != null)
            {
                throw new InvalidOperationException("InfrastructureUnityContainer already configured");
            }

            UnityContainer = container;

            Func<LifetimeManager> newLifeTimeManager = () => Activator.CreateInstance<PerResolveLifetimeManager>();

            UnityContainer.RegisterType<string>("TyrionBankerDBConnectionString", newLifeTimeManager(), new InjectionFactory(c => ConfigurationManager.ConnectionStrings["TyrionBankerDB"].ConnectionString));
            UnityContainer.RegisterType<ITyrionBankerDbConnection, TyrionBankerDbConnection>("TyrionBankerDB", newLifeTimeManager(), 
                new InjectionConstructor(new ResolvedParameter<string>("TyrionBankerDBConnectionString")));
           
            UnityContainer.RegisterTypes(
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractRepository))), 
                WithMappings.FromMatchingInterface, 
                getInjectionMembers: t => new InjectionMember[] 
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>()
                });
        }
    }
}
