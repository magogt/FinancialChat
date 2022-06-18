using FinancialChat.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.Infrastructure.Repositories
{
  public static class RepositoriesIoC
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddSingleton<IChatMessageRepository, ChatMessageRepository>();
      services.AddSingleton<IChatRoomRepository, ChatRoomRepository>(); 
    }
  }
}
