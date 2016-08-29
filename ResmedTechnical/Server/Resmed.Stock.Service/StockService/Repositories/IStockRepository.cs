using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Resmed.Stock.Core;
using Resmed.Stock.Model;

namespace Resmed.Stock.Repositories
{
    public interface IStockRepository
    {
        IList<String> GetAllSymbols();

        IList<Site> GetAllSites();

        void CreateFinanceReport(StockFileModel file);

        IList<StockFileModel> GetStockFiles();

        string GetStockFileContent(int id);
    }
}
