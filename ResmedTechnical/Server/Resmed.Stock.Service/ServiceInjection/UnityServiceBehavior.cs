using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace Resmed.Stock.Service.ServiceInjection 
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        private ServiceHost _serviceHost;

        public UnityServiceBehavior()
        {
            InstanceProvider = new UnityInstanceProvider();
        }

        public UnityServiceBehavior(UnityContainer unity)
        {
            InstanceProvider = new UnityInstanceProvider {Container = unity};
        }

        public UnityInstanceProvider InstanceProvider { get; set; }

        #region IServiceBehavior Members

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var serviceBehavior = serviceDescription.Behaviors.Find<ServiceBehaviorAttribute>();

            foreach (
                EndpointDispatcher ed in
                    serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>().SelectMany(cd => cd.Endpoints))
            {
                if (serviceBehavior != null && serviceBehavior.InstanceContextMode == InstanceContextMode.Single)
                {
                    ed.DispatchRuntime.SingletonInstanceContext = new InstanceContext(serviceHostBase);
                }
                InstanceProvider.ServiceType = serviceDescription.ServiceType;
                ed.DispatchRuntime.InstanceProvider = InstanceProvider;
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        public void AddToHost(ServiceHost host)
        {
            if (_serviceHost != null)
            {
                return;
            }
            host.Description.Behaviors.Add(this);
            _serviceHost = host;
        }
    }
}