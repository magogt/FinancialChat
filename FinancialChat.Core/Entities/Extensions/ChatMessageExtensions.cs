

namespace FinancialChat.Core.Entities.Extensions
{
  public static class ChatMessageExtensions
  {
    private const string stockCommand = "/stock=";
    public static string GetStockName(this ChatMessage msg)
    {

      var idx = msg?.Message?.IndexOf(stockCommand, StringComparison.InvariantCultureIgnoreCase) ?? -1;
      var result = "";
      if (idx != -1)
      {
        result = msg?.Message?.Substring(idx + stockCommand.Length) ?? "";
      }
      return result.Trim();
    }
  }
}

