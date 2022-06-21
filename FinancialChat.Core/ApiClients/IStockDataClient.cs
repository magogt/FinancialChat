using FinancialChat.Core.Dtos;

namespace FinancialChat.Core.ApiClients
{
  public interface IStockDataClient
  {
    Task<StockData?> GetStockData(string stock);
  }
}
