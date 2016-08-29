using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.Unity;
using Resmed.Stock.Managers;
using Resmed.Stock.Repositories;
using Resmed.Stock.Service.Common;

namespace Resmed.Stock.Service.ServiceInjection 
{
    public class FrameworkServiceHostFactory : ServiceHostFactory
    {
        protected static readonly UnityContainer UnityContainer;

        static FrameworkServiceHostFactory()
        {
            UnityContainer = InitializeUnity();
            RegisterServices();
        }

        private static void RegisterServices()
        {
            UnityContainer.RegisterType<IStockManager, StockManager>();
            UnityContainer.RegisterType<IStockRepository, StockRepository>();
            UnityContainer.RegisterType<IAsyncHandler, StockAsyncHandler>();
            UnityContainer.RegisterInstance<IUnityContainer>(UnityContainer);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new FrameWorkServiceHost(UnityContainer, serviceType, baseAddresses);
            return host;
        }

        private static UnityContainer InitializeUnity()
        {
            return new UnityContainer();
        }
    }
}
