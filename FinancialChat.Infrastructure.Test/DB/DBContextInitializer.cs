
using FinancialChat.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialChat.Infrastructure.Test.DB
{
  internal static class DBContextInitializer
  {
    public static Infrastructure.DB.DbContext GetContext()
    {

    DbContextOptions<Infrastructure.DB.DbContext> options;
    var builder = new DbContextOptionsBuilder<Infrastructure.DB.DbContext>();
    builder.UseInMemoryDatabase("chat-app");
      options = builder.Options;
      var context = new Infrastructure.DB.DbContext(options);
      context.AddRange(GetChatRooms());
      context.AddRange(GetChatMessages());
      context.SaveChanges();
      return context;
    }

    private static IEnumerable<ChatMessage> GetChatMessages()
    {
      return new List<ChatMessage>
      {
        new ChatMessage {Message =  "Hello World", Date = new DateTime(), RoomName = "room-1", User = ""},
        new ChatMessage {Message =  "Hello Room 1", Date = new DateTime(), RoomName = "room-1", User = ""},
        new ChatMessage {Message =  "Hello Room 2", Date = new DateTime(), RoomName = "room-2", User = ""}
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
