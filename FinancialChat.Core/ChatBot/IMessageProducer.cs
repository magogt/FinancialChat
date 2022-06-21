namespace FinancialChat.Core.ChatBot
{
  public interface IMessageProducer
  {
    void SendMessage<T>(T messageData);
  }
}
