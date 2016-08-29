using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Resmed.MEP.Aspects.Faults;
using Resmed.MEP.Exceptions;
using Resmed.Stock.ClientService;



namespace Resmed.MEP.Mask.ClientService.Faults
{
    public static class FaultHandler
    {
        public static void Handle(FaultException faultException)
        {
            Exception innerException = null;
            if (faultException is FaultException<RecordNotFoundFault>)
            {
                var recordNotFoundFault = faultException as FaultException<RecordNotFoundFault>;
                innerException = new RecordNotFoundException(recordNotFoundFault.Message);
            }
            if (faultException is FaultException<InvalidRecordFault>)
            {
                var invalidRecordFault = faultException as FaultException<InvalidRecordFault>;
                innerException = new InvalidRecordException(invalidRecordFault.Message);
            }

            if (faultException is FaultException<DuplicateRecordFoundFault>)
            {
                var duplicateRecordFault = faultException as FaultException<DuplicateRecordFoundFault>;
                innerException = new DuplicateRecordFoundException(duplicateRecordFault.Message);
            }

            if (innerException == null)
            {
                throw faultException;
            }

            if (innerException == null)
            {
                throw faultException;
            }

            
            throw new StockException("A exception has occured.",innerException);
            
        }
    }
}
