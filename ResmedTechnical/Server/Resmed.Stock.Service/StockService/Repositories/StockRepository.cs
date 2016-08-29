using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using log4net;
using Resmed.Stock.Service;
using Microsoft.Practices.Unity;
using Resmed.Stock.Core;
using Resmed.Stock.Model;


namespace Resmed.Stock.Repositories
{
    public class StockRepository: ServiceBase,IStockRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StockRepository));

        public StockRepository(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public IList<string> GetAllSymbols()
        {
            using (var context = new StockEntities())
            {
                return context.StockSymbols.Select(x => x.Symbol).ToList(); 
            }
        }

        public int GetSymbolIdBySymbol(string symbol)
        {
            using (var context = new StockEntities())
            {
                var firstOrDefault = context.StockSymbols.FirstOrDefault(x => x.Symbol == symbol);
                if (firstOrDefault != null)
                    return firstOrDefault.ID;
            }
            return -1;
        }

        public IList<Site> GetAllSites()
        {
            using (var context = new StockEntities())
            {
                return context.Sites.ToList();
            }
        }

        public void CreateFinanceReport(StockFileModel file)
        {
            using (var context = new StockEntities())
            {
                var stockFile = new StockFile
                {
                    ID = file.Id,
                    FileContent = file.FileContent,
                    ImportedDateTime = file.ImportedDateTime,
                    SiteId = file.SiteId,
                    StockSymbolId = file.StockSymbolId
                };

                context.StockFiles.AddObject(stockFile);
                context.SaveChanges();
            }

        }

        public IList<StockFileModel> GetStockFiles()
        {
            using (var context = new StockEntities())
            {
                var stockFiles = from f in context.StockFiles
                    select new StockFileModel
                    {
                        Id = f.ID,
                        SiteId = f.SiteId,
                        ImportedDateTime = f.ImportedDateTime,
                        Symbol = context.StockSymbols.FirstOrDefault(x => x.ID == f.StockSymbolId).Symbol,
                        SiteName = context.Sites.FirstOrDefault(x => x.ID == f.SiteId).Name,
                        FileFormat = context.Sites.FirstOrDefault(x => x.ID == f.SiteId).Format
                    };
                return stockFiles.ToList();
            }
        }

        public string GetStockFileContent(int id)
        {
            using (var context = new StockEntities())
            {
                var firstOrDefault = context.StockFiles.FirstOrDefault(x => x.ID == id);
                if (firstOrDefault != null)
                    return firstOrDefault.FileContent;
            }
            return null;
        }
    }
}