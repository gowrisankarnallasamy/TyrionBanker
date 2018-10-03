using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.ApplicationService;
using TyrionBanker.Domain.Domain;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.PolicyInjection;
using Unity.Interception.PolicyInjection.Policies;
using Unity.Registration;
using Unity.RegistrationByConvention;
using Unity.Resolution;

namespace TyrionBanker.Domain
{
    public static class DomainUnityContainer
    {
        internal static IUnityContainer UnityContainer;
        public static void BuildUp(IUnityContainer container)
        {
            if (UnityContainer != null)
            {
                throw new InvalidOperationException("DomainUnityContainer already configured");
            }

            UnityContainer = container;

            UnityContainer.RegisterTypes(
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractApplicationService))),
                WithMappings.FromMatchingInterface,
                getInjectionMembers: t => new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>()
                });

            UnityContainer.RegisterTypes(
                Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(AbstractDomainService))),
                WithMappings.FromMatchingInterface,
                getInjectionMembers: t => new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>()
                });
        }

        public static T Resolve<T>(params ResolverOverride[] overrides) => UnityContainer.Resolve<T>(overrides);

        public static T Resolve<T>(string name, params ResolverOverride[] overrides) => UnityContainer.Resolve<T>(name, overrides);

        public static bool IsRegistered<T>() => UnityContainer.IsRegistered<T>();

        public static bool IsRegistered<T>(string nameToCheck) => UnityContainer.IsRegistered<T>(nameToCheck);
    }
}
