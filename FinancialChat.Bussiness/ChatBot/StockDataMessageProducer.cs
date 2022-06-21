using FinancialChat.Core.ChatBot;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FinancialChat.Bussiness.ChatBot
{
  public class StockDataMessageProducer : IMessageProducer
  {
    const string queueName = "financial-chat";
    private readonly string connectionString;

    public StockDataMessageProducer(IConfiguration config)
    {
      connectionString = config["rabbitMQConnection"];
    }
    public void SendMessage<ChatMessage>(ChatMessage messageData)
    {
      var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
      using var connection = factory.CreateConnection();
      using var channel = connection.CreateModel();
      channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);     
      var message = JsonConvert.SerializeObject(messageData);     
      var body = Encoding.UTF8.GetBytes(message);
      channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);      
    }
  }
}
