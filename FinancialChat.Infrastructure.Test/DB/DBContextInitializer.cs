
using FinancialChat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialChat.Infrastructure.Test.DB
{
  internal static class DBContextInitializer
  {
    static Infrastructure.DB.ChatDbContext? context;
    public static Infrastructure.DB.ChatDbContext GetContext()
    {
      if (context != null) return context;

      DbContextOptions<Infrastructure.DB.ChatDbContext> options;
      var builder = new DbContextOptionsBuilder<Infrastructure.DB.ChatDbContext>();
      builder.UseInMemoryDatabase("chat-app");
      options = builder.Options;
      context = new Infrastructure.DB.ChatDbContext(options);
      context.AddRange(GetChatRooms());
      context.AddRange(GetChatMessages());
      context.SaveChanges();
      return context;
    }

    private static IEnumerable<ChatMessage> GetChatMessages()
    {
      return new List<ChatMessage>
      {
        new ChatMessage {Message =  "Hello World", Date = new DateTime(), RoomId = 1, User = ""},
        new ChatMessage {Message =  "Hello Room 1", Date = new DateTime(), RoomId = 1, User = ""},
        new ChatMessage {Message =  "Hello Room 2", Date = new DateTime(), RoomId = 2, User = ""}
      };
    }

    private static IEnumerable<ChatRoom> GetChatRooms()
    {
      return new List<ChatRoom>
      {
        new ChatRoom {Name = "room-1"},
        new ChatRoom {Name = "room-2"},
        new ChatRoom { Name = "room-3"}
      };
    }
  }
}
