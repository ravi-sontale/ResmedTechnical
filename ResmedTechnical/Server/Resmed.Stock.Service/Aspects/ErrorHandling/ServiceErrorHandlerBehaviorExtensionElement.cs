using System;
using System.ServiceModel.Configuration;

namespace Resmed.Stock.Service
{
    public class ServiceErrorHandlerBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof (ServiceErrorHandler); }
        }

        protected override object CreateBehavior()
        {
            return new ServiceErrorHandler();
        }
    }
}