using Microsoft.AspNetCore.SignalR;

namespace FinancialChat.Hubs
{
  public class FinancialChatHub : Hub
  {
    public async Task AddToGroup(string group)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public  async Task SendMessage(string group, string user, string message)
    {
      await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    public async Task LeaveGroup(string group)
    {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }    
  }
}
