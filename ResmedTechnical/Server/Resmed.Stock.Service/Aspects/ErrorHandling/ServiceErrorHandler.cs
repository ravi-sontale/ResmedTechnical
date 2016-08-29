using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Resmed.MEP.Aspects.Faults;
using Resmed.MEP.Exceptions;
using Resmed.MEP.Common.Validator;
using log4net;

namespace Resmed.Stock.Service
{
    public class ServiceErrorHandler : ServiceErrorHandlerBehaviorExtensionElement, IErrorHandler, IServiceBehavior
    {
        #region IErrorHandler Members

        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceErrorHandler));


        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (!(error is FaultException))
            {
                MessageFault msgFault=null;
                String faultAction = null;

                switch (error.GetType().Name)
                {
                    case "InvalidRecordException":
                        {
                            var fdValidation = new InvalidRecordFault((InvalidRecordException)error);
                            msgFault = new FaultException<InvalidRecordFault>(fdValidation, error.Message, new FaultCode("Service")).CreateMessageFault();
                            break;
                        }
                    case "RecordNotFoundException":
                        {
                            var fdRecordNotFound = new RecordNotFoundFault((RecordNotFoundException)error);
                            msgFault = new FaultException<RecordNotFoundFault>(fdRecordNotFound, error.Message, new FaultCode("Service")).CreateMessageFault();
                            break;
                        }
                    case "DuplicateRecordFoundException":
                        {
                            var fdDuplicateRecordFound = new DuplicateRecordFoundFault((DuplicateRecordFoundException)error);
                            msgFault = new FaultException<DuplicateRecordFoundFault>(fdDuplicateRecordFound, error.Message, new FaultCode("Service")).CreateMessageFault();
                            break;
                        }
                   default:
                        {
                            var errorMessage = String.Format("An unkown error has occured. \n Error Stack: {0}", error.ToString());
                            log.ErrorFormat(errorMessage);
                            EntityValidator.WriteEventLog(errorMessage);                            
                            msgFault = new FaultException<UnknownFault>(new UnknownFault(error), errorMessage, new FaultCode("Service")).CreateMessageFault();
                            break;
                        }
                }

                fault = Message.CreateMessage(version, msgFault, faultAction);
            }
        }

        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channDisp in serviceHostBase.ChannelDispatchers)
            {
                channDisp.ErrorHandlers.Add(this);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            return;
        }

        #endregion
    }
}