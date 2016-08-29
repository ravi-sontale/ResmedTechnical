using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using Resmed.Stock.ClientService;
using Resmed.Stock.ClientService.Managers;

namespace Resmed.Stock.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var manager = new StockManager();
            var stockFiles = manager.GetStockFiles();

            return View(stockFiles.Select(Models.StockModel.FromApiModel).ToList());
             
        }

        public ActionResult Import()
        {
            var manager = new StockManager();
            manager.CreateFinanceReport();
            var stockFiles = manager.GetStockFiles();

            return View(stockFiles.Select(Models.StockModel.FromApiModel).ToList());
        }

        public ActionResult Details(int id, string extenstion)
        {
            var manager = new StockManager();
            var encoding = new UTF8Encoding();
            var contentAsBytes = encoding.GetBytes(manager.GetStockFileContent(id));

            HttpContext.Response.ContentType = "text/plain";
            HttpContext.Response.AddHeader("Content-Disposition", "filename=" + id + "_" + DateTime.Now.ToShortDateString() + "." + extenstion);
            HttpContext.Response.Buffer = true;
            HttpContext.Response.Clear();
            HttpContext.Response.OutputStream.Write(contentAsBytes, 0, contentAsBytes.Length);
            HttpContext.Response.OutputStream.Flush();
            HttpContext.Response.End();

            return View();

        } 
    }
}
