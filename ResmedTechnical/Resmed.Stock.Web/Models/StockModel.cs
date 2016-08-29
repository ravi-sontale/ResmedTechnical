using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resmed.Stock.ClientService.ResmedStockServiceProxy;
namespace Resmed.Stock.Web.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Symbol { get; set; }
        public DateTime ImportedDateTime { get; set; }
        public int SiteId { get; set; }
        public int StockSymbolId { get; set; }
        public string FileContent { get; set; }
        public string FileFormat { get; set; }

        public static StockModel FromApiModel(StockFileModel item)
        {
            return new StockModel
            {
                Id = item.Id,
                SiteName = item.SiteName,
                Symbol = item.Symbol,
                ImportedDateTime = item.ImportedDateTime,
                SiteId = item.SiteId,
                StockSymbolId = item.StockSymbolId,
                FileContent = item.FileContent,
                FileFormat = item.FileFormat
            };
        }
    }
}