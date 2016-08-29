using System;

namespace Resmed.Stock.Model
{
    public class StockFileModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Symbol{ get; set; }
        public DateTime ImportedDateTime { get; set; }
        public int SiteId { get; set; }
        public int StockSymbolId { get; set; }
        public string FileContent { get; set; }
        public string FileFormat { get; set; }
    }
}