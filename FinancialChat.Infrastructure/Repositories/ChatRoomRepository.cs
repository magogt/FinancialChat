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
      var result = 0;
      var item = ctx.ChatRooms.FirstOrDefault(x => x.Id == id);
      if (item != null)
      {
        ctx.Remove(item);
        result = await ctx.SaveChangesAsync();
      }
      return result;
    }

    public Task<IEnumerable<ChatRoom>> GetAll()
    {
      return Task.FromResult( ctx.ChatRooms.AsEnumerable());
    }

    public async Task<ChatRoom> Insert(ChatRoom chatRoom)
    {
      ctx.Add(chatRoom);
      await ctx.SaveChangesAsync();
      return chatRoom;
    }

    public async Task<int> Update(ChatRoom chatRoom)
    {
      var result = 0;
      var id = chatRoom.Id;
      var item = ctx.ChatRooms.FirstOrDefault(x => x.Id == id);
      if (item != null)
      {
        item.Name = chatRoom.Name;        
        result = await ctx.SaveChangesAsync();
      }
      return result;
    }
  }
}
