using CsvHelper;
using FinancialChat.Core.ApiClients;
using FinancialChat.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Globalization;

namespace FinancialChat.Infrastructure.ApiClients
{
  public class StockDataClient : IStockDataClient
  {
    private readonly string url;

    public StockDataClient(IConfiguration config)
    {
      url = config["stockDataApiUrl"];
    }
    public async Task<StockData?> GetStockData(string stock)
    {
      try
      {
        var client = new RestClient($"{url}&s={stock}");
        var request = new RestRequest("", Method.Get);
        var data = await client.DownloadDataAsync(request);
        File.WriteAllBytes(stock + ".csv", data);
        if (data != null)
        {
          using var reader = new StreamReader(new MemoryStream(data));
          using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
          var records = csv.GetRecords<StockData>();
          return records?.FirstOrDefault();
        }
        return null;
      }
      catch (Exception e)
      {
        return null;
      }
    }
  }
}
