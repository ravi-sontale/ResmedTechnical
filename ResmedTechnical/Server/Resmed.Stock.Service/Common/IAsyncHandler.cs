using Resmed.Stock.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resmed.Stock.Service.Common
{
    public interface IAsyncHandler
    {
        void CreateReport(IEnumerable<Site> sites, IList<string> symbols);
    }
}