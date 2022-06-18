using FinancialChat.Core.Entities;

namespace FinancialChat.Core.Repositories
{
  public interface IChatRoomRepository
  {
    Task<IEnumerable<ChatRoom>> GetAll();
    Task<ChatRoom> Insert(ChatRoom chatRoom);
    Task<int> Update(ChatRoom chatRoom);
    Task<int> Delete(int id);
  }
}
