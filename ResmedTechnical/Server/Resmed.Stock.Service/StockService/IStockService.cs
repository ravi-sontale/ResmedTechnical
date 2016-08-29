using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Resmed.Stock.Core;
using Resmed.Stock.Model;

namespace Resmed.Stock.Service
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IStockService
    {
        [OperationContract]
        IList<String> GetAllSymbols();

        [OperationContract]
        IList<Site> GetAllSites();

        [OperationContract]
        void CreateFinanceReport();

        [OperationContract]
        IList<StockFileModel> GetStockFiles();

        [OperationContract]
        string GetStockFileContent(int id);
    }
}
