using FinancialChat.Bussiness.ChatBot;
using FinancialChat.Bussiness.Services;
using FinancialChat.Core.ApiClients;
using FinancialChat.Infrastructure.ApiClients;
using FinancialChat.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FinancialChat.Bot
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // Add Configuration
      var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile($"appsettings.json");

      // Configure DI
      var serviceProvider = new ServiceCollection()
        .AddSingleton<IConfiguration>(configuration.Build())
        .AddScoped<FinancialChatHubService>()
        .AddScoped<StockDataMessageConsumer>()
        .AddScoped<IStockDataClient, StockDataClient>()
        .BuildServiceProvider();

      var stockDataConsumer = serviceProvider.GetService<StockDataMessageConsumer>();
      stockDataConsumer.Start();

      Console.WriteLine($"Chat Bot started, waiting for messages!!");
      Console.ReadKey();
    }
  }
}
