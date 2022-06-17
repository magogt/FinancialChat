using FinancialChat.Core.Entities;
using FinancialChat.Core.Repositories;

namespace FinancialChat.Infrastructure.Repositories
{
  public class ChatMessageRepository : IChatMessageRepository
  {
    public Task Delete(ChatMessage chatMessage)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatMessage>> GetByRoom(string room)
    {
      throw new NotImplementedException();
    }

    public Task Insert(ChatMessage chatMessage)
    {
      throw new NotImplementedException();
    }

    public Task Update(ChatMessage chatMessage)
    {
      throw new NotImplementedException();
    }
  }
}
