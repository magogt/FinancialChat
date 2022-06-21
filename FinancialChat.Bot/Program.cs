using FinancialChat.Bussiness.ChatBot;
using FinancialChat.Bussiness.Services;
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
        .BuildServiceProvider();

      var stockDataConsumer = serviceProvider.GetService<StockDataMessageConsumer>();
      stockDataConsumer.Start();

      Console.WriteLine($"Press any key to exit");
      Console.ReadKey();
    }
  }
}
