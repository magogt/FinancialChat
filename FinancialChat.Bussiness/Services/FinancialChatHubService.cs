using FinancialChat.Core.Entities;
using FinancialChat.Core.Repositories;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace FinancialChat.Bussiness.Services
{
  public class FinancialChatHubService
  {
    private HubConnection? hubConnection;
    private readonly string hubUrl;
    private readonly IChatMessageRepository messageRepository;

    public FinancialChatHubService(IConfiguration config, IChatMessageRepository messageRepository)
    {
      hubUrl = config["chatHubUrl"] ?? "";
      this.messageRepository = messageRepository;
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
      var msg = await messageRepository.Insert(message);
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
