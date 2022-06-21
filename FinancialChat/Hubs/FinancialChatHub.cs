using FinancialChat.Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FinancialChat.Hubs
{
  public class FinancialChatHub : Hub
  {
    public async Task AddToGroup(string group)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public  async Task SendMessage(ChatMessage message)
    {
      await Clients.Group(message.RoomId.ToString()).SendAsync("ReceiveMessage", message);
    }

    public async Task LeaveGroup(string group)
    {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }    
  }
}
