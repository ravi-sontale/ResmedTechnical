using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resmed.Stock.ClientService.ResmedStockServiceProxy;
using Resmed.Stock.ClientService.Common;
using Resmed.MEP.Mask.ClientService.Faults;
using System.ServiceModel;

namespace Resmed.Stock.ClientService.Managers
{
    public class StockManager : IStockManager
    {
        public IList<string> GetAllSymbols()
        {
            List<String> symbols = null;
            try
            {
                using (WcfClientProxy<StockServiceClient, IStockService> proxy = WcfHelper.CreateServiceProxy())
                {
                    symbols = proxy.MakeCall(c => c.GetAllSymbols());
                }
            }
            catch (FaultException ex)
            {
                FaultHandler.Handle(ex);
            }

            return symbols;
        }


        public IList<Site> GetAllSites()
        {
            List<Site> sites = null;
            try
            {
                using (WcfClientProxy<StockServiceClient, IStockService> proxy = WcfHelper.CreateServiceProxy())
                {
                    sites = proxy.MakeCall(c => c.GetAllSites());
                }
            }
            catch (FaultException ex)
            {
                FaultHandler.Handle(ex);
            }

            return sites;
        }

        public void CreateFinanceReport()
        {
            try
            {
                using (WcfClientProxy<StockServiceClient, IStockService> proxy = WcfHelper.CreateServiceProxy())
                {
                    proxy.MakeCall(c => c.CreateFinanceReport());
                }
            }
            catch (FaultException ex)
            {
                FaultHandler.Handle(ex);
            }
        }

        public IEnumerable<StockFileModel> GetStockFiles()
        {

            IEnumerable<StockFileModel> stockFiles = null;
            try
            {
                using (WcfClientProxy<StockServiceClient, IStockService> proxy = WcfHelper.CreateServiceProxy())
                {
                    stockFiles = proxy.MakeCall(c => c.GetStockFiles());
                }
            }
            catch (FaultException ex)
            {
                FaultHandler.Handle(ex);
            }

            return stockFiles;
        }

        public string GetStockFileContent(int id)
        {

            var content = string.Empty;
            try
            {
                using (WcfClientProxy<StockServiceClient, IStockService> proxy = WcfHelper.CreateServiceProxy())
                {
                    content = proxy.MakeCall(c => c.GetStockFileContent(id));
                }
            }
            catch (FaultException ex)
            {
                FaultHandler.Handle(ex);
            }

            return content;
        }
    }
}