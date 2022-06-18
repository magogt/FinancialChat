

namespace FinancialChat.Core.Entities
{
  public class ChatMessage
  {
    public int Id { get; set; }
    public string? User { get; set; }
    public string? RoomName { get; set; }
    public string? Message { get; set; }
    public DateTime? Date { get; set; }
  }
}
