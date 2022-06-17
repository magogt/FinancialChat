using FinancialChat.Core.Entities;

namespace FinancialChat.Core.Repositories
{
  public interface IChatRoomRepository
  {
    Task<IEnumerable<ChatRoom>> GetAll();
    Task Insert(ChatRoom chatRoom);
    Task Update(ChatRoom chatRoom);
    Task Delete(ChatRoom chatRoom);
  }
}
