using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Resmed.Stock.Service;

namespace Resmed.Stock.Service.ServiceInjection 
{
    public class FrameWorkServiceHost : ServiceHost
    {
        private readonly UnityContainer _container;

        public FrameWorkServiceHost()
        {
            _container = new UnityContainer();
        }

        public FrameWorkServiceHost(UnityContainer container, Type serviceType, Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _container = container;
        }

        public IUnityContainer Container
        {
            get { return _container; }
        }

        protected override void OnOpening()
        {
            if (Description.Behaviors.Find<UnityServiceBehavior>() == null)
            {
                Description.Behaviors.Add(new UnityServiceBehavior(_container));
            }
            if (Description.Behaviors.Find<ServiceErrorHandler>() == null)
            {
                Description.Behaviors.Add(new ServiceErrorHandler());
            }
            base.OnOpening();
        }
    }
}