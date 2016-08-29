using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resmed.Stock.ClientService.Managers;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace Resmed.Stock.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void GetAllSymbols()
        {  
            var manager = new StockManager();
            var symbols = manager.GetAllSymbols();
            Assert.IsTrue(symbols.Count > 0);
        }

        [TestMethod]
        public void GetAllSites()
        {
            var manager = new StockManager();
            var sites = manager.GetAllSites();
            Assert.IsTrue(sites.Count > 0);
        }

        [TestMethod]
        public void GetStockFiles()
        {
            var manager = new StockManager();
            var stockFiles = manager.GetStockFiles();
            Assert.IsTrue(stockFiles.ToList().Count > 0);
        }
    }


   
}
