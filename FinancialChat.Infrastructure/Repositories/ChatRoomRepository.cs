using FinancialChat.Core.Entities;
using FinancialChat.Core.Repositories;
using FinancialChat.Infrastructure.DB;

namespace FinancialChat.Infrastructure.Repositories
{
  public class ChatRoomRepository : IChatRoomRepository
  {
    private readonly DbContext ctx;

    public ChatRoomRepository(DbContext ctx)
    {
      this.ctx = ctx;
    }

    public async Task<int> Delete(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<ChatRoom>> GetAll()
    {
      throw new NotImplementedException();
    }

    public async Task<ChatRoom> Insert(ChatRoom chatRoom)
    {
      throw new NotImplementedException();
    }

    public async Task<int> Update(ChatRoom chatRoom)
    {
      throw new NotImplementedException();
    }
  }
}
