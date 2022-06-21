using FinancialChat.Core.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace FinancialChat.Bussiness.Services
{
  public class FinancialChatHubService
  {
    private HubConnection? hubConnection;
    private readonly string hubUrl;

    public FinancialChatHubService(IConfiguration config)
    {
      hubUrl = config["chatHubUrl"] ?? "";
    }
    public async Task<HubConnection> Start()
    {

      if (hubConnection == null)
      {
        hubConnection = new HubConnectionBuilder()
              .WithUrl(hubUrl)
              .WithAutomaticReconnect()
              .Build();
        await hubConnection.StartAsync();
      }
      return hubConnection;
    }

    public async Task JoinGroup(int group)
    {
      if (hubConnection != null)
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
