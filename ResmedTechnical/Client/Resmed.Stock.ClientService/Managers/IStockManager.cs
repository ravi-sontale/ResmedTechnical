using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resmed.Stock.ClientService.ResmedStockServiceProxy;

namespace Resmed.Stock.ClientService.Managers
{
    public interface IStockManager
    {
        IList<String> GetAllSymbols();

        IList<Site> GetAllSites();

        void CreateFinanceReport();

        IEnumerable<StockFileModel> GetStockFiles();
    }
}
