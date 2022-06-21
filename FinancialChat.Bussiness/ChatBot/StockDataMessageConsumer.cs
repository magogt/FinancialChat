using FinancialChat.Bussiness.Services;
using FinancialChat.Core.ChatBot;
using FinancialChat.Core.Entities;
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
    private IConnection? connection;
    private IModel? channel;
    private ManualResetEvent resetEvent = new ManualResetEvent(false);

    public StockDataMessageConsumer(IConfiguration config, FinancialChatHubService chatHubService)
    {
      connectionString = config["rabbitMQConnection"];
      this.chatHubService = chatHubService;
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
        Console.WriteLine("** Received message: {0} by Consumer thread **", message);
        var msg = JsonConvert.DeserializeObject<ChatMessage>(message);
        msg.Message = msg.Message += " processed!";
        await chatHubService.SendMessage(msg);
        channel.BasicAck(deliveryEventArgs.DeliveryTag, false);
      };

      // start consuming
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
