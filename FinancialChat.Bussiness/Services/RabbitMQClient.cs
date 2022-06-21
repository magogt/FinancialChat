using RabbitMQ.Client;

namespace FinancialChat.Bussiness.Services
{
  public class RabbitMQClient
  {
    public IModel GetConnection(string queue)
    {
      var factory = new ConnectionFactory() { Uri = new Uri("amqps://kukwareg:Qzsj9D_T1gNrp4MHcq2nHE8lVQoYgNft@moose.rmq.cloudamqp.com/kukwareg") };
      var connection = factory.CreateConnection();
      var channel = connection.CreateModel();      
      channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
      return channel;      
    }
  }
}
