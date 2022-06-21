using FinancialChat.Core.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace FinancialChat.Bussiness.Services
{
  public class FinancialChatHubService
  {
    private HubConnection? hubConnection;

    public async Task<HubConnection> Start(string url)
    {
      if (hubConnection == null)
      {
        hubConnection = new HubConnectionBuilder()
              .WithUrl(url)
              .WithAutomaticReconnect()
              .Build();
        await hubConnection.StartAsync();
      }
      return hubConnection;
    }

    public async Task JoinGroup(int group)
    {
      if(hubConnection != null)
      {
        await hubConnection.InvokeAsync("AddToGroup", group.ToString());
      }
    }

    public async Task SendMessage(ChatMessage message)
    {
      if (hubConnection != null)
      {
        await hubConnection.InvokeAsync("SendMessage", message);
      }
    }

    public async Task LeaveGroup(int group)
    {
      if (hubConnection != null)
      {
        await hubConnection.InvokeAsync("LeaveGroup", group.ToString());
      }
    }
  }
}
