namespace FinancialChat.Core.Hubs
{
  public interface IFinancialChatHub
  {
    Task Join(string group);
    Task SendMessage(string group, string user, string message);
    Task Disconnect(string group);
  }
}
