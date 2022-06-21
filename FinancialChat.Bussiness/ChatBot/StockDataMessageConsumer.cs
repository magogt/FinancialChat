using FinancialChat.Bussiness.Services;
using FinancialChat.Core.ApiClients;
using FinancialChat.Core.ChatBot;
using FinancialChat.Core.Entities;
using FinancialChat.Core.Entities.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FinancialChat.Bussiness.ChatBot
{
  public class StockDataMessageConsumer : IMessageConsumer, IDisposable
  {
    const string queueName = "financial-chat";
    private readonly string connectionString;
    private readonly FinancialChatHubService chatHubService;
    private readonly IStockDataClient stockDataClient;
    private IConnection? connection;
    private IModel? channel;
    private ManualResetEvent resetEvent = new ManualResetEvent(false);

    public StockDataMessageConsumer(IConfiguration config, FinancialChatHubService chatHubService, IStockDataClient stockDataClient)
    {
      connectionString = config["rabbitMQConnection"];
      this.chatHubService = chatHubService;
      this.stockDataClient = stockDataClient;
    }


    public void Start()
    {
      var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
      connection = factory.CreateConnection();
      channel = connection.CreateModel();
      channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

      chatHubService?.Start();

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += async (model, deliveryEventArgs) =>
      {
        var body = deliveryEventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var msg = JsonConvert.DeserializeObject<ChatMessage>(message);
        var stock = msg.GetStockName();
        Console.WriteLine("** Processing quote: {0} **", stock);
        var stockData = await stockDataClient.GetStockData(stock);

        if (stockData != null)
        {
          msg.Message = $"{stockData.Symbol} quote is ${stockData.Close ?? stockData.Open} per share";
          Console.WriteLine("**** {0}", msg.Message);
          await chatHubService.SendMessage(msg);
        }else
          {
          Console.WriteLine("**** Couldn't get data");
          }
        channel.BasicAck(deliveryEventArgs.DeliveryTag, false);
      };
      _ = channel.BasicConsume(consumer, queueName);
    }

    public void Dispose()
    {
      resetEvent.WaitOne();
      channel?.Close();
      channel = null;
      connection?.Close();
      connection = null;
    }
  }
}
