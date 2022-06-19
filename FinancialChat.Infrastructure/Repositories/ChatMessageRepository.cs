using FinancialChat.Core.Entities;
using FinancialChat.Core.Repositories;
using FinancialChat.Infrastructure.DB;

namespace FinancialChat.Infrastructure.Repositories
{
  public class ChatMessageRepository : IChatMessageRepository
  {
    private readonly DbContext ctx;

    public ChatMessageRepository(DbContext ctx)
    {
      this.ctx = ctx;
    }

    public async Task<int> Delete(int id)
    {
      var result = 0;
      var item = ctx.ChatMessages.FirstOrDefault(x => x.Id == id);
      if (item != null)
      {
        ctx.Remove(item);
        result = await ctx.SaveChangesAsync();
      }
      return result;
    }

    public Task<IEnumerable<ChatMessage>> GetByRoom(int room)
    {
      return Task.FromResult(ctx.ChatMessages.Where(x => x.RoomId == room).AsEnumerable());
    }

    public async Task<ChatMessage> Insert(ChatMessage chatMessage)
    {
      ctx.Add(chatMessage);
      await ctx.SaveChangesAsync();
      return chatMessage;
    }

    public async Task<int> Update(ChatMessage chatMessage)
    {
      var result = 0;
      var id = chatMessage.Id;
      var item = ctx.ChatMessages.FirstOrDefault(x => x.Id == id);
      if (item != null)
      {
        item.Message = chatMessage.Message;
        item.Date = chatMessage.Date;
        item.RoomId = chatMessage.RoomId;
        item.User = chatMessage.User;
        result = await ctx.SaveChangesAsync();
      }
      return result;
    }
  }
}
