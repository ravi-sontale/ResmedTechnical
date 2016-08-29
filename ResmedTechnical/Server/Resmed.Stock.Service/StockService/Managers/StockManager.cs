using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resmed.Stock.Service;
using log4net;
using Microsoft.Practices.Unity;

namespace Resmed.Stock.Managers
{
    public class StockManager:ServiceBase, IStockManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StockManager));

        public StockManager(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }
    }
}