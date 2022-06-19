

namespace FinancialChat.Core.Entities
{
  public class ChatMessage
  {
    public int Id { get; set; }
    public string? User { get; set; }
    public int RoomId { get; set; }
    public string? Message { get; set; }
    public DateTime? Date { get; set; }
  }
}
