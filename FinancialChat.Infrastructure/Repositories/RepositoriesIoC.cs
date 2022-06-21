using FinancialChat.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.Infrastructure.Repositories
{
  public static class RepositoriesIoC
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
      services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
      return services;
    }
  }
}
