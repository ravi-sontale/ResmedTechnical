using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Resmed.Stock.Service;
using log4net;
using Microsoft.Practices.Unity;
using Resmed.Stock.Repositories;
using Resmed.Stock.Core;
using System.IO;
using System.Threading;
using Resmed.Stock.Model;
using Resmed.Stock.Service.Common;


namespace Resmed.Stock.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class StockService : ServiceBase, IStockService
    {
        /// <summary>LogForNet Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(StockService));
      

        public StockService(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public IList<String> GetAllSymbols()
        {
            var repository = UnityContainer.Resolve<StockRepository>();
            return repository.GetAllSymbols(); 
        }

        public IList<Site> GetAllSites()
        {
            var repository = UnityContainer.Resolve<StockRepository>();
            return repository.GetAllSites(); 
        }

        public void CreateFinanceReport()
        {
            var asyncHandler = UnityContainer.Resolve<StockAsyncHandler>();
            asyncHandler.CreateReport(GetAllSites(), GetAllSymbols());
        }

        public IList<StockFileModel> GetStockFiles()
        {
            var repository = UnityContainer.Resolve<StockRepository>();
            return repository.GetStockFiles(); 
        }

        public string GetStockFileContent(int id)
        {
             var repository = UnityContainer.Resolve<StockRepository>();
             return repository.GetStockFileContent(id); 
                
        }
       
    }
}
