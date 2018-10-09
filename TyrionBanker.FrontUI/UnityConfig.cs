using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core;
using TyrionBanker.FrontUI.Common;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Interception.PolicyInjection;
using Unity.Registration;
using Unity.RegistrationByConvention;
using Unity.Resolution;

namespace TyrionBanker.FrontUI
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> LazyContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                Assembly.GetExecutingAssembly().GetTypes().Where(p => typeof(ITyrionBankerBase).IsAssignableFrom(p)),
                WithMappings.FromAllInterfacesInSameAssembly,
                getInjectionMembers: t => new InjectionMember[]
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<PolicyInjectionBehavior>()
                });
        }

        public static IUnityContainer UnityContainer
        {
            get
            {
                return LazyContainer.Value;
            }
        }

        public static T Resolve<T>(params ResolverOverride[] overrides) => UnityContainer.Resolve<T>(overrides);

        public static T Resolve<T>(string name, params ResolverOverride[] overrides) => UnityContainer.Resolve<T>(name, overrides);

        public static bool IsRegistered<T>() => UnityContainer.IsRegistered<T>();

        public static bool IsRegistered<T>(string nameToCheck) => UnityContainer.IsRegistered<T>(nameToCheck);
    }
}
