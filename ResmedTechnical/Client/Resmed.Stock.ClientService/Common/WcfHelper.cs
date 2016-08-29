using Resmed.Stock.ClientService.ResmedStockServiceProxy;
namespace Resmed.Stock.ClientService.Common
{
    internal static class WcfHelper
    {

        public static WcfClientProxy<StockServiceClient, IStockService> CreateServiceProxy()
        {
            return new WcfClientProxy<StockServiceClient, IStockService>();
        }
    }
}