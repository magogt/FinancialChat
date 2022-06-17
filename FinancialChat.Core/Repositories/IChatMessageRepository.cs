
using FinancialChat.Core.Entities;

namespace FinancialChat.Core.Repositories
{
  public interface IChatMessageRepository
  {
    Task<IEnumerable<ChatMessage>> GetByRoom(string room);
    Task Insert(ChatMessage chatMessage);
    Task Update(ChatMessage chatMessage);
    Task Delete(ChatMessage chatMessage);
  }
}
