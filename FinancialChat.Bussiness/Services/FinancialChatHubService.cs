using Microsoft.AspNetCore.SignalR.Client;

namespace FinancialChat.Core.Services
{
  public class FinancialChatHubService
  {
    private HubConnection? hubConnection;

    public async Task<HubConnection> Connect(string url)
    {
      hubConnection = new HubConnectionBuilder()
            .WithUrl(url)
            .WithAutomaticReconnect()
            .Build();
      await hubConnection.StartAsync();
      return hubConnection;
    }

    public async Task JoinGroup(string group)
    {
      if(hubConnection != null)
      {
        await hubConnection.InvokeAsync("AddToGroup", group);
      }
    }

    public async Task SendMessage(string group, string user, string message)
    {
      if (hubConnection != null)
      {
        await hubConnection.InvokeAsync("SendMessage", group, user, message);
      }
    }

    public async Task LeaveGroup(string group)
    {
      if (hubConnection != null)
      {
        await hubConnection.InvokeAsync("LeaveGroup", group);
      }
    }
  }
}
