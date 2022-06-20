using FinancialChat.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.Infrastructure.Repositories
{
  public static class RepositoriesIoC
  {
    public static void AddRepositories(this IServiceCollection services)
    {
      services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
      services.AddScoped<IChatRoomRepository, ChatRoomRepository>(); 
    }
  }
}
