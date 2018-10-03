using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;
using TyrionBanker.ApiWeb;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Exceptions;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TyrionBanker.ApiWeb.UnityMvcActivator), nameof(TyrionBanker.ApiWeb.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TyrionBanker.ApiWeb.UnityMvcActivator), nameof(TyrionBanker.ApiWeb.UnityMvcActivator.Shutdown))]

namespace TyrionBanker.ApiWeb
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            //DependencyResolver.SetResolver(new UnityResolver2(UnityConfig.Container));
            UnityConfig.Container.RegisterType<IControllerFactory, UnityControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(UnityConfig.Container.Resolve<IControllerFactory>());

            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }

    public class UnityResolver2 : System.Web.Http.Dependencies.IDependencyResolver, IServiceLocator
    {
        protected IUnityContainer container;

        public UnityResolver2(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver2(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public object GetInstance(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return null;
            }
        }

        public object GetInstance(Type serviceType, string key)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>()
        {
            try
            {
                return (TService)container.Resolve(typeof(TService));
            }
            catch (ResolutionFailedException)
            {
                return default(TService);
            }
        }

        public TService GetInstance<TService>(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
        }
    }

    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;

        public UnityControllerFactory(IUnityContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    return null;
                }

                return (IController)_container.Resolve(controllerType);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}