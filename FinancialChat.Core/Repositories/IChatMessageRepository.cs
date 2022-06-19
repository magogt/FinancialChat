
using FinancialChat.Core.Entities;

namespace FinancialChat.Core.Repositories
{
  public interface IChatMessageRepository
  {
    Task<IEnumerable<ChatMessage>> GetByRoom(int room);
    Task<ChatMessage> Insert(ChatMessage chatMessage);
    Task<int> Update(ChatMessage chatMessage);
    Task<int> Delete(int id);
  }
}
